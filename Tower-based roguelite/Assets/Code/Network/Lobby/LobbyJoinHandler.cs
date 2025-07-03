using FishNet.Transporting;
using UnityEngine;
using FishNet;
using FishNet.Connection;

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
            return;

        Debug.Log($"[FishNet] Cliente conectado: {conn.ClientId}");

        if (SceneLoader.Instance == null)
        {
            Debug.LogError("[LobbyJoinHandler] SceneLoader.Instance no encontrado.");
            return;
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
    }
}