using System;

namespace UnchordMetroidvania
{
    [Serializable]
    public class BoxRangeBattleSkillOption
    {
        public int level = 0;
        public int targetCount = 1;
        public float baseDamage = 1.0f;
        public TargetSortType sortType = TargetSortType.None;
        public bool canDetectSelf = false;
        public LTRB range;
        public int cooltime = 0;
    }
}