using UnityEngine;

public class HitContext
{
    public GameObject attacker;
    public GameObject target;
    public float damage;
    public Vector3 hitDirection;
    public ActionData sourceAction;

    public HitContext(GameObject attacker, GameObject target, ActionData action)
    {
        this.attacker = attacker;
        this.target = target;
        this.damage = action.damage;
        this.hitDirection = (target.transform.position - attacker.transform.position).normalized;
        this.sourceAction = action;
    }
}