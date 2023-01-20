namespace UnchordMetroidvania
{
    public sealed class EntityAnimationEnd<T> : EntityStateEvent<T>
    where T : EntityBase
    {
        public EntityAnimationEnd(T instance, EntityStateBT<T> state)
        : base(instance, state)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            state.OnAnimationEnd();
            return InvokeResult.Success;
        }
    }
}