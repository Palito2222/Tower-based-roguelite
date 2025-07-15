using FishNet.Connection;
using System;
using UnityEngine;

public class SimpleMatchmaker : MonoBehaviour
{
    public void AssignInstance(NetworkConnection conn)
    {
        // Ejemplo: jugador solo -> nueva GUID
        var newId = Guid.NewGuid();
        conn.FirstObject.GetComponent<InstanceManager>().ChangeInstanceServerRpc(newId);
    }
}