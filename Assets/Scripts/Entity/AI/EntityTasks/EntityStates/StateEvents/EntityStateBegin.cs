namespace UnchordMetroidvania
{
    public sealed class EntityStateBegin<T> : EntityStateEvent<T>
    where T : EntityBase
    {
        public EntityStateBegin(T instance, EntityStateBT<T> state)
        : base(instance, state)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            state.OnStateBegin();
            return InvokeResult.Success;
        }
    }
}