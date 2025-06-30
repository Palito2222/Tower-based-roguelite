using UnityEngine;

public class HitboxVisualizer : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public Quaternion rotation;
    public float duration = 0.2f;
    public Color color = Color.red;

    private float timer;

    public void Initialize(Vector3 center, Vector3 size, Quaternion rotation, float duration, Color color)
    {
        this.center = center;
        this.size = size;
        this.rotation = rotation;
        this.duration = duration;
        this.color = color;
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
            Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(center, rotation, Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}