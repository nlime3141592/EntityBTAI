namespace UnchordMetroidvania
{
    public sealed class EntityOnUpdate<T> : EntityStateEvent<T>
    where T : EntityBase
    {
        public EntityOnUpdate(T instance, EntityStateBT<T> state)
        : base(instance, state)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            return state.OnUpdate();
        }
    }
}