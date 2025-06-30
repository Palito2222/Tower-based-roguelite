using System.Collections.Generic;
using UnityEngine;

public static class HitScanner
{
    public static List<IHittable> Scan(ActionData data, Transform origin)
    {
        List<IHittable> results = new List<IHittable>();

        if (data.shape == "Box")
        {
            Vector3 forward = origin.forward;
            Vector3 center = origin.position + forward * data.range / 2;
            Vector3 size = new Vector3(data.width, 1, data.range);

            // Dibujo de debug
            HitGizmoDrawer.DrawBox(center, size, origin.rotation);

            Collider[] hits = Physics.OverlapBox(center, size * 0.5f, origin.rotation);
            foreach (var col in hits)
            {
                if (col.TryGetComponent<IHittable>(out var h)) results.Add(h);
            }
        }

        return results;
    }
}