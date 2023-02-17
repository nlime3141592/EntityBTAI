using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public static class EntitySensor
    {
        public static Collider2D[] OverlapBox(EntityBase origin, LTRB range, EntitySensorGizmoOption gizmo, int layerMask)
        {
            Vector2 pStart = origin.transform.position;
            Vector2 pEnd = pStart;

            float lx = origin.lookDir.x;
            float ly = origin.lookDir.y;

            pStart -= new Vector2(lx * range.left, ly * range.bottom);
            pEnd += new Vector2(lx * range.right, ly * range.top);

            Collider2D[] colliders = Physics2D.OverlapAreaAll(pStart, pEnd, layerMask);

            if(gizmo.bShowGizmo)
                origin.skillRangeGizmoManager.Add(new EntityBoxSensorGizmo(gizmo.duration, gizmo.color, pStart, pEnd));

            return colliders;
        }
    }
}