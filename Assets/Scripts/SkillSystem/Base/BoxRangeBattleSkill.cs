using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class BoxRangeBattleSkill : RangeBattleSkill
    {
        public LTRB range;

        public BoxRangeBattleSkill(
            string name, int id, int level,
            int targetCount,
            TargetSortType sortType, bool canDetectSelf,
            LTRB range
        )
        : base(
            name, id, level,
            targetCount,
            sortType, canDetectSelf)
        {
            this.range = range;
        }

        public override EntityBase[] GetTargets(EntityBase executor)
        {
            Collider2D[] colliders = m_Detect(executor);
            List<EntityBase> entities = m_FilterEntities(executor, colliders);
            return entities?.ToArray();
        }

        private Collider2D[] m_Detect(EntityBase executor)
        {
            Vector2 pStart = executor.transform.position;
            Vector2 pEnd = pStart;

            float lx = executor.lookDir.x;
            float ly = executor.lookDir.y;
            int layerMask = 1 << LayerMask.NameToLayer("Entity");

            pStart -= new Vector2(lx * range.left, ly * range.bottom);
            pEnd += new Vector2(lx * range.right, ly * range.top);

            Collider2D[] colliders = Physics2D.OverlapAreaAll(pStart, pEnd, layerMask);

            if(bRangeOnEditor)
                executor.skillRangeGizmoManager.Add(new BoxRangeGizmo(timesRangeOnEditor, Color.cyan, pStart, pEnd));

            return colliders;
        }

        private List<EntityBase> m_FilterEntities(EntityBase executor, Collider2D[] colliders)
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
                else if(!bCanDetectSelf && obj == executor.gameObject)
                    continue;
                else if(obj.TryGetComponent<EntityBase>(out entity))
                    entities.Add(entity);
            }

            return entities;
        }
    }
}