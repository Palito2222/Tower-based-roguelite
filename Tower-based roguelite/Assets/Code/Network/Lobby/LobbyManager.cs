using UnityEngine;
using FishNet.Object;
using FishNet.Managing.Scened;
using UnityEngine.SceneManagement;
using System.Collections;
using FishNet;
using FishNet.Component.Scenes;

public class LobbyManager : NetworkBehaviour
{
    [SerializeField] private string roomSceneName = "GameRoomBase";

    public override void OnStartServer()
    {
        base.OnStartServer();
        SceneManager.OnLoadEnd += OnSceneLoadComplete;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        SceneManager.OnLoadEnd -= OnSceneLoadComplete;
    }

    private void OnSceneLoadComplete(SceneLoadEndEventArgs data)
    {
        Debug.Log($"OnSceneLoadComplete: {data.LoadedScenes.Length} escenas cargadas.");
        bool foundTargetScene = false;
        foreach (Scene loadedScene in data.LoadedScenes)
        {
            Debug.Log($"Escena cargada: {loadedScene.name}, isLoaded: {loadedScene.isLoaded}, handle: {loadedScene.handle}");
            if (loadedScene.name == roomSceneName && IsServerInitialized)
            {
                foundTargetScene = true;
                StartCoroutine(WaitAndSetActiveScene(loadedScene.name));
                break;
            }
        }
        if (!foundTargetScene)
        {
            Debug.LogError($"No se encontró la escena {roomSceneName} en las escenas cargadas.");
            // Listar todas las escenas actuales para depuración
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
                Debug.Log($"Escena actual {i}: {scene.name}, isLoaded: {scene.isLoaded}, handle: {scene.handle}");
            }
        }
    }

    private IEnumerator WaitAndSetActiveScene(string sceneName)
    {
        Debug.Log($"Esperando carga de escena: {sceneName}");

        Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
        int maxAttempts = 100;
        int attempt = 0;

        while (scene.handle == 0 || !scene.isLoaded)
        {
            attempt++;
            if (attempt > maxAttempts)
            {
                Debug.LogError($"Tiempo de espera agotado para la escena {sceneName}. No se encontró o no se cargó.");
                for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
                {
                    Scene s = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
                    Debug.Log($"Escena actual {i}: {s.name}, isLoaded: {s.isLoaded}, handle: {s.handle}");
                }
                yield break;
            }

            scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
            Debug.Log($"Intento {attempt}: Escena {sceneName} aún no cargada (handle: {scene.handle}, isLoaded: {scene.isLoaded})");
            yield return null;
        }

        try
        {
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
            Debug.Log($"Escena activada: {scene.name}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error al activar la escena {sceneName}: {ex.Message}");
        }
    }

    public void OnCreateRoomButton()
    {
        if (!IsServerInitialized)
        {
            Debug.LogError("Servidor no inicializado.");
            return;
        }

        Debug.Log($"Configurando y cargando escena: {roomSceneName}");

        // Configurar la escena online en DefaultScene
        var defaultScene = InstanceFinder.NetworkManager.GetComponent<DefaultScene>();
        if (defaultScene != null)
        {
            defaultScene.SetOnlineScene(roomSceneName);
            Debug.Log($"Escena online configurada en DefaultScene: {roomSceneName}");
        }
        else
        {
            Debug.LogError("Componente DefaultScene no encontrado en NetworkManager.");
            return;
        }

        // Descargar escenas no deseadas
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
            if (scene.name != roomSceneName && scene.isLoaded && scene.name != "MovedObjectsHolder")
            {
                Debug.Log($"Descargando escena no deseada: {scene.name}");
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
            }
        }

        // Carga escena globalmente en servidor
        SceneLoadData sldGlobal = new SceneLoadData(roomSceneName);
        sldGlobal.ReplaceScenes = ReplaceOption.All;
        SceneManager.LoadGlobalScenes(sldGlobal);
        Debug.Log($"Carga global iniciada para {roomSceneName}");

        // Carga escena para cliente específico
        SceneLoadData sldConnection = new SceneLoadData(roomSceneName);
        sldConnection.ReplaceScenes = ReplaceOption.All;
        SceneManager.LoadConnectionScenes(Owner, sldConnection);
        Debug.Log($"Carga para cliente iniciada para {roomSceneName}");
    }
}