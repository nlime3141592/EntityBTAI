using System;

// 개발 보류
namespace UnchordMetroidvania
{
    public abstract class FiniteStateMachineNodeBT<T> : ControlNodeBT<T>
    {
        protected FiniteStateMachineNodeBT(T instance, int capacity)
        : base(instance, capacity)
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