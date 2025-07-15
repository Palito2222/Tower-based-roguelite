using FishNet.Connection;
using FishNet.Managing.Scened;
using FishNet;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

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
        bool already = InstanceFinder.SceneManager.SceneConnections
                       .Any(kvp => kvp.Key.name == "MasterLobby" &&
                                   kvp.Value.Contains(conn));
        if (already) return;

        var sld = new SceneLoadData("MasterLobby")
        {
            ReplaceScenes = ReplaceOption.All,
            Options = new LoadOptions
            {
                AutomaticallyUnload = true,
                AllowStacking = false,    // <— ¡DESACTIVADO!
            }
        };
        InstanceFinder.SceneManager.LoadConnectionScenes(conn, sld);
    }

    public void LoadLobbyForGroup(List<NetworkConnection> group)
    {
        SceneLoadData sld = new SceneLoadData("MasterLobby")
        {
            ReplaceScenes = ReplaceOption.All
        };

        NetworkConnection[] array = group.ToArray();

        InstanceFinder.SceneManager.LoadConnectionScenes(array, sld);
    }

    public static bool ConnectionHasLobby(NetworkConnection conn)
    {
        // ¿Existe alguna entrada cuya clave sea una escena llamada MasterLobby
        // y cuyo Value contenga a ‘conn’?
        return InstanceFinder.SceneManager.SceneConnections
               .Any(kvp => kvp.Key.name == "MasterLobby" &&
                           kvp.Value.Contains(conn));
    }
}