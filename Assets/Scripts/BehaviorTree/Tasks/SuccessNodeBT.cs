namespace UnchordMetroidvania
{
    public sealed class SuccessNodeBT<T> : ResultNodeBT<T>
    {
        public SuccessNodeBT(T instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke() => InvokeResult.Success;
    }
}