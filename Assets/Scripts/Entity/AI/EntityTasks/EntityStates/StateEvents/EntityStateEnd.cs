namespace UnchordMetroidvania
{
    public sealed class EntityStateEnd<T> : EntityStateEvent<T>
    where T : EntityBase
    {
        public EntityStateEnd(T instance, EntityStateBT<T> state)
        : base(instance, state)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            state.OnStateEnd();
            return InvokeResult.Success;
        }
    }
}