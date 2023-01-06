using System;

namespace UnchordMetroidvania
{
    public abstract class FiniteStateMachineNodeBT<T> : CompositeNodeBT<T>
    {
        protected FiniteStateMachineNodeBT(ConfigurationBT<T> config, int id, string name, int initCapacity)
        : base(config, id, name, initCapacity)
        {

        }

        protected sealed override InvokeResult p_Invoke()
        {
            int iPrevChild = childIndex;
            int iNextChild = GetNextChildIndex();

            childIndex = iNextChild;

            if(childIndex < 0)
                return InvokeResult.Failure;

            return children[iNextChild].Invoke();
        }

        protected abstract int GetNextChildIndex();
    }
}