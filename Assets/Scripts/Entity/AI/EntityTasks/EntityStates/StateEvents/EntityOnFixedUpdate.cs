namespace UnchordMetroidvania
{
    public sealed class EntityOnFixedUpdate<T> : EntityStateEvent<T>
    where T : EntityBase
    {
        public EntityOnFixedUpdate(T instance, EntityStateBT<T> state)
        : base(instance, state)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            state.OnFixedUpdate();
            return InvokeResult.Success;
        }
    }
}