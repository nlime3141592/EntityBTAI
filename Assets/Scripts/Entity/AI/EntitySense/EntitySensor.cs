using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public static class EntitySensor
    {
        public readonly static int entityLayerMask = 1 << LayerMask.NameToLayer("Entity");

        public static Collider2D[] OverlapBox(EntityBase origin, LTRB range, EntitySensorGizmoOption gizmo)
        {
            Vector2 pStart = origin.transform.position;
            Vector2 pEnd = pStart;

            float lx = origin.lookDir.x;
            float ly = origin.lookDir.y;

            pStart -= new Vector2(lx * range.left, ly * range.bottom);
            pEnd += new Vector2(lx * range.right, ly * range.top);

            Collider2D[] colliders = Physics2D.OverlapAreaAll(pStart, pEnd, entityLayerMask);

            if(gizmo.bShowGizmo)
                origin.skillRangeGizmoManager.Add(new EntityBoxSensorGizmo(gizmo.duration, gizmo.color, pStart, pEnd));

            return colliders;
        }
    }
}