namespace UnchordMetroidvania
{
    public sealed class EntityActionEnd<T> : EntityStateEvent<T>
    where T : EntityBase
    {
        public EntityActionEnd(T instance, EntityStateBT<T> state)
        : base(instance, state)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            state.OnActionEnd();
            return InvokeResult.Success;
        }
    }
}