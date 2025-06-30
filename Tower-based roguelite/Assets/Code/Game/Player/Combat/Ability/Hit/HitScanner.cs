using System.Collections.Generic;
using UnityEngine;

public static class HitScanner
{
    public static List<IHittable> Scan(ActionData data, Transform origin)
    {
        var list = new List<IHittable>();

        Vector3 offset = new Vector3(data.offsetX, data.offsetY, data.offsetZ);
        Vector3 forward = origin.forward;
        Quaternion rotation = origin.rotation;

        if (data.shape == "Box")
        {
            Vector3 halfExtents = new Vector3(data.width / 2f, data.height / 2f, data.depth / 2f);
            Vector3 center = origin.position + rotation * offset;

            Collider[] hits = Physics.OverlapBox(center, halfExtents, rotation);
            foreach (var col in hits)
            {
                if (col.TryGetComponent<IHittable>(out var h)) list.Add(h);
            }

            // Visual
            CreateDebugHitbox(center, halfExtents * 2f, rotation, Color.red);
        }

        return list;
    }

    private static void CreateDebugHitbox(Vector3 center, Vector3 size, Quaternion rotation, Color color)
    {
        var go = new GameObject("HitboxDebug");
        var vis = go.AddComponent<HitboxVisualizer>();
        vis.Initialize(center, size, rotation, 0.3f, color);
    }
}
