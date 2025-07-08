using FishNet.Transporting;
using UnityEngine;
using FishNet;
using FishNet.Connection;
using System.Collections;

public class LobbyJoinHandler : MonoBehaviour
{
    private void Start()
    {
        InstanceFinder.ServerManager.OnRemoteConnectionState += OnClientConnected;
    }

    private void OnDestroy()
    {
        if (InstanceFinder.ServerManager != null)
            InstanceFinder.ServerManager.OnRemoteConnectionState -= OnClientConnected;
    }

    private void OnClientConnected(NetworkConnection conn, RemoteConnectionStateArgs args)
    {
        if (args.ConnectionState != RemoteConnectionState.Started)
        {
            Debug.Log("Estamos jodidos señor.");
            return;
        }

        Debug.Log($"[FishNet] Cliente conectado: {conn.ClientId}");

        StartCoroutine(DelayedSceneLoad(conn));
    }

    private IEnumerator DelayedSceneLoad(NetworkConnection conn)
    {
        // Espera ligera para evitar conflictos (puedes subirla si sigue fallando)
        yield return new WaitForSeconds(0.25f);

        if (SceneLoader.Instance == null)
        {
            Debug.LogError("[LobbyJoinHandler] SceneLoader.Instance no encontrado.");
            yield break;
        }

        if (PartyManager.Instance != null && PartyManager.Instance.IsInParty(conn))
        {
            var group = PartyManager.Instance.GetParty(conn);
            SceneLoader.Instance.LoadLobbyForGroup(group);
        }
        else
        {
            SceneLoader.Instance.LoadLobbyForConnection(conn);
        }

        Debug.Log($"[LobbyJoinHandler] Llamado a LoadLobby para cliente {conn.ClientId} tras delay.");
    }
}