using System;

namespace UnchordMetroidvania
{
    public abstract class TaskNodeBT<T> : NodeBT<T>
    {
        public TaskNodeBT(T instance)
        : base(instance)
        {

        }
    }
}