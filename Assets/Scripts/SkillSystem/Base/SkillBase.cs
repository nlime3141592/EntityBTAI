namespace UnchordMetroidvania
{
    public abstract class SkillBase
    {
        public readonly string name;
        public readonly int id;
        public int level;
        public float cooltime = 0;

        public SkillBase(string name, int id, int level)
        {
            this.name = name;
            this.id = id;
            this.level = level;
        }
    }
}