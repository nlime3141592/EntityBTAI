namespace UnchordMetroidvania
{
    public sealed class EntityAnimationBegin<T> : EntityStateEvent<T>
    where T : EntityBase
    {
        public EntityAnimationBegin(T instance, EntityStateBT<T> state)
        : base(instance, state)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            state.OnAnimationBegin();
            return InvokeResult.Success;
        }
    }
}