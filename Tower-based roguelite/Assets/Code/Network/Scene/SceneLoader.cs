using FishNet.Connection;
using FishNet.Managing.Scened;
using FishNet;
using UnityEngine;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void LoadLobbyForConnection(NetworkConnection conn)
    {
        string lobbySceneName = "MasterLobby";

        SceneLoadData sld = new SceneLoadData(lobbySceneName)
        {
            ReplaceScenes = ReplaceOption.All
        };

        InstanceFinder.SceneManager.LoadConnectionScenes(conn, sld);
    }

    public void LoadLobbyForGroup(List<NetworkConnection> group)
    {
        string lobbySceneName = "MasterLobby";

        SceneLoadData sld = new SceneLoadData(lobbySceneName)
        {
            ReplaceScenes = ReplaceOption.All
        };

        NetworkConnection[] array = group.ToArray();

        InstanceFinder.SceneManager.LoadConnectionScenes(array, sld);
    }
}