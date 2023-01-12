namespace UnchordMetroidvania
{
    public abstract class EntityTask<T> : TaskNodeBT<T>
    where T : EntityBase
    {
        public EntityTask(T instance)
        : base(instance)
        {

        }
    }
}