using System.Collections.Generic;
using FishNet.Connection;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance { get; private set; }

    // Aqu� se asigna cada conexi�n a su grupo de party
    private Dictionary<NetworkConnection, List<NetworkConnection>> partyGroups = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CreateParty(NetworkConnection leader, NetworkConnection guest)
    {
        List<NetworkConnection> group = new() { leader, guest };
        foreach (var conn in group)
            partyGroups[conn] = group;
    }

    public List<NetworkConnection> GetParty(NetworkConnection conn)
    {
        return partyGroups.TryGetValue(conn, out var group) ? group : new List<NetworkConnection> { conn };
    }

    public bool IsInParty(NetworkConnection conn)
    {
        return partyGroups.ContainsKey(conn);
    }
}