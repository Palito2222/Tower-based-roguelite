using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Title("Stats del Jugador")]
    public float health = 100f;
    public float damage = 10f;
    public float speed = 5f;
    public float jump = 1.2f;

    public void PrintStats()
    {
        Debug.Log($"[Stats] Vida: {health}, Daño: {damage}, Velocidad: {speed}, Salto: {jump}");
    }
}
