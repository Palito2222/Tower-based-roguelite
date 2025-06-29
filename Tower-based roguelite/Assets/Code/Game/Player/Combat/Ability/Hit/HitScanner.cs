using System.Collections.Generic;
using UnityEngine;

public static class HitScanner
{
    public static List<IHittable> Scan(ActionData data, Transform origin)
    {
        var list = new List<IHittable>();
        if (data.shape == "Box")
        {
            Vector3 dir = origin.forward;
            Vector3 center = origin.position + dir * data.range / 2;
            Vector3 halfExtents = new Vector3(data.width / 2, 1, data.range / 2);

            Collider[] hits = Physics.OverlapBox(center, halfExtents, origin.rotation);
            foreach (var col in hits)
            {
                if (col.TryGetComponent<IHittable>(out var h)) list.Add(h);
            }
        }
        return list;
    }
}