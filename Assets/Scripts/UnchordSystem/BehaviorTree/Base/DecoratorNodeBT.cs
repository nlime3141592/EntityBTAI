namespace UnchordMetroidvania
{
    public abstract class DecoratorNodeBT<T> : CompositeNodeBT<T>
    {
        public DecoratorNodeBT(T instance)
        : base(instance, 1)
        {

        }
    }
}