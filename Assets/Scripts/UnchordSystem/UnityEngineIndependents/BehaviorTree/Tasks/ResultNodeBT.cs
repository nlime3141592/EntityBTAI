namespace UnchordMetroidvania
{
    public abstract class ResultNodeBT<T> : TaskNodeBT<T>
    {
        protected ResultNodeBT(T instance)
        : base(instance)
        {

        }
    }
}