using FishNet.Observing;
using FishNet.Connection;
using UnityEngine;

[CreateAssetMenu(menuName = "FishNet/Observers/InstanceCondition", fileName = "InstanceCondition")]
public class InstanceCondition : ObserverCondition
{
    public override bool ConditionMet(NetworkConnection connection, bool currentlyAdded, out bool notProcessed)
    {
        notProcessed = false;
        // Obtener el NetworkObject al que pertenece esta condición
        var nobj = NetworkObject;
        if (nobj == null)
            return false;

        // Componente InstanceManager en el objeto
        if (!nobj.TryGetComponent<InstanceManager>(out var imObj))
            return false;

        // Componente InstanceManager en el jugador (primer objeto del connection)
        var playerObj = connection.FirstObject;
        if (playerObj == null || !playerObj.TryGetComponent<InstanceManager>(out var imPlayer))
            return false;

        // Solo pasar si coinciden los InstanceId
        return imObj.InstanceId.Value == imPlayer.InstanceId.Value;
    }

    public override ObserverConditionType GetConditionType()
    {
        return ObserverConditionType.Normal;
    }
}