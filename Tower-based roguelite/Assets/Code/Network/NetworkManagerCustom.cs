using FishNet.Managing;
using UnityEngine;

public class NetworkManagerCustom : MonoBehaviour
{
    private NetworkManager net;

    private void Awake()
    {
        net = FindObjectOfType<NetworkManager>();
        StartHost();
    }

    public void StartHost()
    {
        net.ServerManager.StartConnection();
        net.ClientManager.StartConnection();
    }

    public void StartClient()
    {
        net.ClientManager.StartConnection();
    }

    public void StartServerOnly()
    {
        net.ServerManager.StartConnection();
    }
}