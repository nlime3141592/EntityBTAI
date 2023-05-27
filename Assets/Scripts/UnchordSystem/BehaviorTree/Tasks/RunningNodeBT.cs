namespace UnchordMetroidvania
{
    public sealed class RunningNodeBT<T> : ResultNodeBT<T>
    {
        public RunningNodeBT(T instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke() => InvokeResult.Running;
    }
}