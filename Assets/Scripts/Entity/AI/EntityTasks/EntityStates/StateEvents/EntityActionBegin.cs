namespace UnchordMetroidvania
{
    public sealed class EntityActionBegin<T> : EntityStateEvent<T>
    where T : EntityBase
    {
        public EntityActionBegin(T instance, EntityStateBT<T> state)
        : base(instance, state)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            state.OnActionBegin();
            return InvokeResult.Success;
        }
    }
}