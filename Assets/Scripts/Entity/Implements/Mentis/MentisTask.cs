namespace UnchordMetroidvania
{
    public abstract class MentisTask : TaskNodeBT<Mentis>
    {
        public MentisTask(ConfigurationBT<Mentis> config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}