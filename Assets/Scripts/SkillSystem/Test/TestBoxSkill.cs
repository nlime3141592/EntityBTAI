using UnityEngine;

namespace UnchordMetroidvania
{
    public class TestBoxSkill : BoxRangeBattleSkill
    {
        public TestBoxSkill(
            string name, int id, int level,
            int targetCount,
            TargetSortType sortType, bool canDetectSelf,
            LTRB range
        )
        : base(
            name, id, level,
            targetCount,
            sortType, canDetectSelf,
            range
        )
        {

        }

        public override _EntityBase[] GetTargets(_EntityBase executor)
        {
            Debug.Log("Use Test Box Skill.");
            return base.GetTargets(executor);
        }
    }
}