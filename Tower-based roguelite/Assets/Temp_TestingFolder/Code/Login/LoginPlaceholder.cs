using UnityEngine;

public class LoginPlaceholder : MonoBehaviour
{
    private NetworkManagerCustom net;

    private void Awake()
    {
        net = FindObjectOfType<NetworkManagerCustom>();
    }

    public void OnLoginPlayer()
    {
        net.StartClient();
    }

    public void OnLoginAdmin()
    {
        net.StartHost();
    }
}
