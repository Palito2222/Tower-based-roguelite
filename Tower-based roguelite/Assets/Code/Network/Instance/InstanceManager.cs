using FishNet.Object;
using FishNet.Connection;
using System;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class InstanceManager : NetworkBehaviour
{
    // Identificador único de instancia (shard/party)
    public readonly SyncVar<Guid> InstanceId = new SyncVar<Guid>();

    public override void OnStartServer()
    {
        // Asignar una nueva instancia si aún no tiene ID
        if (InstanceId.Value == Guid.Empty)
            InstanceId.Value = Guid.NewGuid();
    }

    // Permitir reasignar instancias (ej. al formar party)
    [ServerRpc(RequireOwnership = false)]
    public void ChangeInstanceServerRpc(Guid newId)
    {
        InstanceId.Value = newId;
    }

    // Método de utilidad para invitar a otro jugador
    public void InviteToParty(NetworkConnection target)
    {
        // Al invitar, reasignamos el mismo ID
        ChangeInstanceServerRpc(InstanceId.Value);
        // Aquí podrías enviar un mensaje al cliente 'target' para que ejecute UnClientRpc...
    }

    void OnGUI()
    {
        if (InstanceId.Value != Guid.Empty)
            GUI.Label(new Rect(10, 10, 400, 20), $"InstanceId: {InstanceId.Value}");
    }
}