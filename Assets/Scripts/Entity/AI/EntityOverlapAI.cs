using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public static class EntityOverlapAI
    {
        public static EntityBase[] GetEntities(EntityBase origin, LTRB range, bool bCanDetectSelf, int layerMask, bool showGizmo, params string[] tags)
        {
            Collider2D[] colliders = s_m_OverlapBox(origin, range, layerMask, showGizmo);
            List<EntityBase> entities = s_m_FilterEntities(origin, colliders, bCanDetectSelf, tags);
            return entities?.ToArray() ?? new EntityBase[0];
        }

        private static Collider2D[] s_m_OverlapBox(EntityBase origin, LTRB range, int layerMask, bool showGizmo)
        {
            Vector2 pStart = origin.transform.position;
            Vector2 pEnd = pStart;

            float lx = origin.lookDir.x;
            float ly = origin.lookDir.y;

            pStart -= new Vector2(lx * range.left, ly * range.bottom);
            pEnd += new Vector2(lx * range.right, ly * range.top);

            Collider2D[] colliders = Physics2D.OverlapAreaAll(pStart, pEnd, layerMask);

            if(showGizmo)
                origin.skillRangeGizmoManager.Add(new BoxRangeGizmo(0.1f, Color.cyan, pStart, pEnd));

            return colliders;
        }

        private static List<EntityBase> s_m_FilterEntities(EntityBase origin, Collider2D[] colliders, bool bCanDetectSelf, params string[] tags)
        {
            List<EntityBase> entities = new List<EntityBase>(colliders.Length);
            bool contains = false;
            GameObject obj = null;
            EntityBase entity = null;

            for(int i = 0; i < colliders.Length; ++i)
            {
                obj = colliders[i].gameObject;
                contains = false;

                for(int j = 0; j < entities.Count && !contains; ++j)
                    contains = (obj == entities[j].gameObject);

                if(contains)
                    continue;
                else if(tags != null && !s_m_bCheckTag(obj, tags))
                    continue;
                else if(!bCanDetectSelf && obj == origin.gameObject)
                        continue;
                else if(obj.TryGetComponent<EntityBase>(out entity))
                    entities.Add(entity);
            }

            return entities;
        }

        private static bool s_m_bCheckTag(GameObject obj, string[] tags)
        {
            if(tags.Length == 0)
                return true;

            for(int i = 0; i < tags.Length; ++i)
                if(obj.tag == tags[i])
                    return true;

            return false;
        }
    }
}