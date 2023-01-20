namespace UnchordMetroidvania
{
    public abstract class EntityStateEvent<T> : TaskNodeBT<T>
    where T : EntityBase
    {
        protected EntityStateBT<T> state { get; private set; }

        public EntityStateEvent(T instance, EntityStateBT<T> state)
        : base(instance)
        {
            this.state = state;
        }
    }
}