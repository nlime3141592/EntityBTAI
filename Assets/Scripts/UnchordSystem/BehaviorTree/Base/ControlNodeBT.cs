using System;

namespace UnchordMetroidvania
{
    public abstract class ControlNodeBT<T> : CompositeNodeBT<T>
    {
        public ControlNodeBT(T instance, int capacity = 2)
        : base(instance, capacity)
        {
            
        }
    }
}