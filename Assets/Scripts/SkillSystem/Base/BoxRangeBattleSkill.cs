using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class BoxRangeBattleSkill : RangeBattleSkill
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

        public override _EntityBase[] GetTargets(_EntityBase executor)
        {
            Collider2D[] colliders = m_Detect(executor);
            List<_EntityBase> entities = m_FilterEntities(colliders);
            return entities.ToArray();
        }

        private Collider2D[] m_Detect(_EntityBase executor)
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

        private List<_EntityBase> m_FilterEntities(Collider2D[] colliders)
        {
            List<_EntityBase> entities = new List<_EntityBase>(colliders.Length);
            GameObject obj = default(GameObject);
            bool contains = false;
            int j = 0;

            for(int i = 0; i < colliders.Length; ++i)
            {
                obj = colliders[i].gameObject;
                contains = false;
                j = entities.Count;

                while(!contains && j > 0)
                    contains |= (obj == entities[--j].gameObject);

                if(!contains)
                    entities.Add(obj.GetComponent<_EntityBase>());
            }

            return entities;
        }
    }
}