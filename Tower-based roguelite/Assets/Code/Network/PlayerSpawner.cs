using FishNet.Object;
using FishNet.Managing.Scened;
using FishNet;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private NetworkObject playerPrefab;

    private void Awake()
    {
        InstanceFinder.SceneManager.OnLoadEnd += OnLoadEnd;
    }

    private void OnDestroy()
    {
        if (InstanceFinder.SceneManager != null)
            InstanceFinder.SceneManager.OnLoadEnd -= OnLoadEnd;
    }

    private void OnLoadEnd(SceneLoadEndEventArgs args)
    {
        foreach (Scene scene in args.LoadedScenes)
        {
            StartCoroutine(SpawnWhenSceneReady(scene));
        }
    }

    private IEnumerator SpawnWhenSceneReady(Scene scene)
    {
        const float timeout = 5f;
        float timer = 0f;
        HashSet<NetworkConnection> connections = null;

        Debug.Log($"[PlayerSpawner] Esperando a que '{scene.name}' se registre en SceneConnections...");

        // Esperar hasta que la escena esté registrada y tenga conexiones
        while (!InstanceFinder.SceneManager.SceneConnections.TryGetValue(scene, out connections) || connections.Count == 0)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            if (timer > timeout)
            {
                Debug.LogError($"[PlayerSpawner] TIMEOUT: La escena '{scene.name}' no se registró correctamente en SceneConnections.");
                yield break;
            }
        }

        Debug.Log($"[PlayerSpawner] '{scene.name}' registrado. Conexiones en escena: {connections.Count}");

        foreach (var conn in connections)
        {
            if (conn.FirstObject != null)
            {
                Debug.Log($"[PlayerSpawner] Cliente {conn.ClientId} ya tiene jugador instanciado.");
                continue;
            }

            NetworkObject player = Instantiate(playerPrefab);
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(player.gameObject, scene);

            try
            {
                InstanceFinder.ServerManager.Spawn(player, conn);
                Debug.Log($"[PlayerSpawner] Jugador {conn.ClientId} spawneado correctamente en {scene.name}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[PlayerSpawner] Error al spawnear jugador para {conn.ClientId}: {ex.Message}");
            }
        }
    }
}
