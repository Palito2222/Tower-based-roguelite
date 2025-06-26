using FishNet.Managing.Scened;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneProcessor : SceneProcessorBase
{
    private List<AsyncOperation> _operations = new();

    public MySceneProcessor()
    {
        _operations = new List<AsyncOperation>(); // Asegura inicialización
    }

    public override void BeginLoadAsync(string sceneName, LoadSceneParameters parameters)
    {
        Debug.Log($"Iniciando carga asíncrona de la escena: {sceneName}");
        _operations.Clear();
        var operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, parameters);
        if (operation == null)
        {
            Debug.LogError($"No se pudo iniciar la carga de la escena {sceneName}. Verifica que esté en Build Settings.");
            return;
        }
        operation.allowSceneActivation = true;
        _operations.Add(operation);
    }

    public override void BeginUnloadAsync(Scene scene)
    {
        _operations.Clear();
        var operation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
        _operations.Add(operation);
    }

    public override IEnumerator AsyncsIsDone()
    {
        foreach (var op in _operations)
        {
            while (!op.isDone)
            {
                Debug.Log($"Progreso de carga para {op}: {op.progress * 100}%");
                yield return null;
            }
        }
        Debug.Log("Todas las operaciones de carga completadas.");
    }

    public override void ActivateLoadedScenes()
    {
        Scene targetScene = default;
        foreach (Scene scene in GetLoadedScenes())
        {
            Debug.Log($"Evaluando escena: {scene.name}, isLoaded: {scene.isLoaded}, handle: {scene.handle}");
            if (scene.name == "GameRoomBase" && scene.isLoaded)
            {
                targetScene = scene;
                break;
            }
        }

        if (targetScene.IsValid() && targetScene.isLoaded)
        {
            try
            {
                UnityEngine.SceneManagement.SceneManager.SetActiveScene(targetScene);
                Debug.Log($"Escena activada desde SceneProcessor: {targetScene.name}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error al activar la escena {targetScene.name}: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró GameRoomBase o no está cargada para activar.");
        }
    }

    public override List<Scene> GetLoadedScenes()
    {
        List<Scene> scenes = new();
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            scenes.Add(UnityEngine.SceneManagement.SceneManager.GetSceneAt(i));
        return scenes;
    }

    public override float GetPercentComplete()
    {
        if (_operations.Count == 0)
            return 1f;

        float total = 0f;
        foreach (var op in _operations)
            total += op.progress;

        return total / _operations.Count;
    }

    public override bool IsPercentComplete()
    {
        if (_operations == null || _operations.Count == 0)
        {
            Debug.LogWarning("No hay operaciones de carga en curso.");
            return true; // Considera la carga completa si no hay operaciones
        }

        foreach (var op in _operations)
        {
            if (op == null)
            {
                Debug.LogWarning("Operación de carga nula encontrada en _operations.");
                continue;
            }
            if (!op.isDone)
                return false;
        }
        return true;
    }
}