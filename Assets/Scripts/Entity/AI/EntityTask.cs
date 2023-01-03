namespace UnchordMetroidvania
{
    public abstract class EntityTask<T> : TaskNodeBT<T>
    where T : EntityBase
    {
        public EntityTask(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}