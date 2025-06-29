using System.Collections.Generic;
using UnityEngine;

public class BuffInventory : MonoBehaviour
{
    [Header("Dependencias")]
    public PlayerStats playerStats;

    [Header("Buffs activos")]
    public List<BuffBase> activeBuffs = new List<BuffBase>();

    public void AddBuff(BuffBase buff)
    {
        if (!activeBuffs.Contains(buff))
        {
            activeBuffs.Add(buff);
            buff.ApplyBuff(playerStats);
            Debug.Log($"[Buff] Aplicado: {buff.buffName}");
            playerStats.PrintStats();
        }
    }
}
