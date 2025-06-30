using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public RectTransform fillBarRect;
    public Transform target;        // El enemigo al que sigue la barra
    public Vector3 offset = new Vector3(0, 2.5f, 0); // Altura sobre el enemigo

    private Camera mainCamera;
    private EnemyHealth enemyHealth;
    private float maxWidth;

    void Start()
    {
        mainCamera = Camera.main;
        enemyHealth = target.GetComponent<EnemyHealth>();
        enemyHealth.OnHealthChanged += UpdateBar; // Suscripción al evento
        maxWidth = fillBarRect.sizeDelta.x;
        UpdateBar(enemyHealth.CurrentHealth, enemyHealth.MaxHealth);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Posicionar el canvas sobre el enemigo, ajustado por offset
        transform.position = target.position + offset;

        // Apuntar la barra hacia la cámara para que siempre sea legible
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }

    void UpdateBar(float current, float max)
    {
        float percent = Mathf.Clamp01(current / max);
        fillBarRect.sizeDelta = new Vector2(maxWidth * percent, fillBarRect.sizeDelta.y);
    }

    private void OnDestroy()
    {
        if (enemyHealth != null)
            enemyHealth.OnHealthChanged -= UpdateBar;
    }
}