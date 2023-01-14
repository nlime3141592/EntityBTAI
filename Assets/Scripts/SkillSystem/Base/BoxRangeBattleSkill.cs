using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class BoxRangeBattleSkill : RangeBattleSkill
    {
        public LTRB range;
        private List<EntityBase> m_targets;

        public BoxRangeBattleSkill(
            string name, int id, int level,
            int targetCount, float baseDamage,
            TargetSortType sortType, bool canDetectSelf,
            LTRB range
        )
        : base(
            name, id, level,
            targetCount, baseDamage,
            sortType, canDetectSelf)
        {
            this.range = range;
            m_targets = new List<EntityBase>(4);
        }

        public BoxRangeBattleSkill(
            string name, int id,
            BoxRangeBattleSkillOption options
        )
        : this(
            name, id, options.level,
            options.targetCount, options.baseDamage,
            options.sortType, options.canDetectSelf,
            options.range
        )
        {
            
        }

        public override EntityBase[] GetTargets(EntityBase executor, params string[] tags)
        {
            EntitySensorGizmoOption gizmo = new EntitySensorGizmoOption();
            gizmo.bShowGizmo = true;
            gizmo.duration = 0.5f;
            gizmo.color = Color.cyan;

            Collider2D[] colTargets = EntitySensor.OverlapBox(executor, range, gizmo);

            m_targets
                .FilterFromColliders(executor, colTargets, bCanDetectSelf, tags)
                .SetTargetCount(targetCount);

            return m_targets.ToArray();
        }

        public void UpdateOptions(BoxRangeBattleSkillOption option)
        {
            base.level = option.level;
            base.targetCount = option.targetCount;
            base.baseDamage = option.baseDamage;
            base.sortType = option.sortType;
            base.bCanDetectSelf = option.canDetectSelf;
            this.range = option.range;
        }
    }
}