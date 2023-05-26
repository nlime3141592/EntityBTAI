namespace UnchordMetroidvania
{
    public sealed class FailureNodeBT<T> : ResultNodeBT<T>
    {
        public FailureNodeBT(T instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke() => InvokeResult.Failure;
    }
}