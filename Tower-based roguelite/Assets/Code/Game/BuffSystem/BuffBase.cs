using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "Game/Buff")]
public class BuffBase : ScriptableObject
{
    [Header("Información del Buff")]
    public string buffName;
    public string description;

    [Header("Efectos del Buff")]
    public float healthBonus;
    public float damageBonus;
    public float speedBonus;
    public float jumpBonus;

    public void ApplyBuff(PlayerStats stats)
    {
        stats.health += healthBonus;
        stats.damage += damageBonus;
        stats.speed += speedBonus;
        stats.jump += jumpBonus;
    }
}

