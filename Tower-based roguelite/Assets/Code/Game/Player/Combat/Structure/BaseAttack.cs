using UnityEngine;

public abstract class BaseAttack : ScriptableObject
{
    public string attackName;
    public float damage;
    public float cooldown;
    public float range;

    public abstract void Execute(Transform origin, GameObject user);
}
