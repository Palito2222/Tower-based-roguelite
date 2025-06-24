using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Combat/Attacks/Melee")]
public class MeleeAttack : BaseAttack
{
    public override void Execute(Transform origin, GameObject user)
    {
        RaycastHit hit;
        if (Physics.Raycast(origin.position, origin.forward, out hit, range))
        {
            var enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}