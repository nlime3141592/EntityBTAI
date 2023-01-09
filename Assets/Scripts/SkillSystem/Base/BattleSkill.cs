namespace UnchordMetroidvania
{
    public abstract class BattleSkill : SkillBase
    {
        public float baseDamage;
        public int targetCount;

        public BattleSkill(
            string name, int id, int level,
            int targetCount, float baseDamage)
        : base(name, id, level)
        {
            if(targetCount < 1)
                targetCount = 1;

            this.targetCount = targetCount;
            this.baseDamage = baseDamage;
        }

        public abstract EntityBase[] GetTargets(EntityBase executor, params string[] tags);
    }
}