using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHittable
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ApplyHit(HitContext context)
    {
        currentHealth -= context.damage;
        Debug.Log($"{gameObject.name} recibió {context.damage} de daño de {context.attacker.name}");

        if (currentHealth <= 0)
            Die();
    }

    public void ApplyEffect(string effectId)
    {
        Debug.Log($"Aplicando efecto: {effectId}");
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        Destroy(gameObject);
    }
}