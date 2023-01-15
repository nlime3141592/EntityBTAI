namespace UnchordMetroidvania
{
    public abstract class RangeBattleSkill : BattleSkill
    {
        public EntitySortType sortType;
        public bool bCanDetectSelf;

        public bool bRangeOnEditor = false;
        public float timesRangeOnEditor = 0.5f;

        public RangeBattleSkill(
            string name, int id, int level,
            int targetCount, float baseDamage,
            EntitySortType sortType, bool canDetectSelf)
        : base(name, id, level, targetCount, baseDamage)
        {
            this.sortType = sortType;
            this.bCanDetectSelf = canDetectSelf;
        }
    }
}