using DamageNumbersPro;
using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHittable
{
    public float MaxHealth = 100f;
    public float CurrentHealth { get; private set; }

    public DamageNumber damageNumeberVFX;
    public GameObject deadExplosionPrefab;

    public event Action<float, float> OnHealthChanged;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public void ApplyHit(HitContext context)
    {
        CurrentHealth -= context.damage;
        CurrentHealth = Mathf.Max(0, CurrentHealth);

        DamageNumber damageNumber = damageNumeberVFX.Spawn(transform.position, context.damage);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        Debug.Log($"{gameObject.name} recibió {context.damage} de daño de {context.attacker.name}");

        if (CurrentHealth <= 0)
            StartCoroutine(Die());
    }

    public void ApplyEffect(string effectId)
    {
        Debug.Log($"Aplicando efecto: {effectId}");
    }

    private IEnumerator Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        GameObject explosionGO = Instantiate(deadExplosionPrefab);
        ParticleSystem explosion = explosionGO.GetComponent<ParticleSystem>();

        yield return new WaitForSeconds(0);
        //Destroy(this.gameObject);
    }
}