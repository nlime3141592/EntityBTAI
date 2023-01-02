namespace UnchordMetroidvania
{
    public abstract class BattleSkill : SkillBase
    {
        public int targetCount;

        public BattleSkill(
            string name, int id, int level,
            int targetCount)
        : base(name, id, level)
        {
            if(targetCount < 1)
                targetCount = 1;

            this.targetCount = targetCount;
        }

        public abstract _EntityBase[] GetTargets(_EntityBase executor);
    }
}