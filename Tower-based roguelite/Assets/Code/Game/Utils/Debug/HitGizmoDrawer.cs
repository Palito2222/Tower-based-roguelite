using UnityEngine;
using System.Collections.Generic;

public class HitGizmoDrawer : MonoBehaviour
{
    private class GizmoHit
    {
        public Vector3 position;
        public Vector3 size;
        public Quaternion rotation;
        public float expireTime;
        public Color color;
    }

    private static List<GizmoHit> gizmos = new();

    public static void DrawBox(Vector3 center, Vector3 size, Quaternion rotation, float duration = 0.5f, Color? color = null)
    {
        gizmos.Add(new GizmoHit
        {
            position = center,
            size = size,
            rotation = rotation,
            expireTime = Time.time + duration,
            color = color ?? Color.red
        });
    }

    private void OnDrawGizmos()
    {
        float now = Time.time;
        for (int i = gizmos.Count - 1; i >= 0; i--)
        {
            if (gizmos[i].expireTime < now)
            {
                gizmos.RemoveAt(i);
                continue;
            }

            Gizmos.color = gizmos[i].color;
            Matrix4x4 rotMatrix = Matrix4x4.TRS(gizmos[i].position, gizmos[i].rotation, Vector3.one);
            Gizmos.matrix = rotMatrix;
            Gizmos.DrawWireCube(Vector3.zero, gizmos[i].size);
        }
    }
}