namespace UnchordMetroidvania
{
    public abstract class MentisAttack : TaskNodeBT<Mentis>
    {
        public MentisAttack(ConfigurationBT<Mentis> config, int id, string name)
        : base(config, id, name)
        {
            
        }
    }
}