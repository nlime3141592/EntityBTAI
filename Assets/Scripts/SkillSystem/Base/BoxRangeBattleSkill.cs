using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class BoxRangeBattleSkill : RangeBattleSkill
    {
        public LTRB range;

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
        }

        public BoxRangeBattleSkill(
            string name, int id,
            BoxRangeBattleSkillOption options
        )
        : base(
            name, id, options.level,
            options.targetCount, options.baseDamage,
            options.sortType, options.canDetectSelf
        )
        {
            this.range = options.range;
        }

        public override EntityBase[] GetTargets(EntityBase executor, params string[] tags)
        {
            return EntityOverlapAI.GetEntities(
                executor, range, bCanDetectSelf,
                1 << LayerMask.NameToLayer("Entity"), bRangeOnEditor, tags
            );
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