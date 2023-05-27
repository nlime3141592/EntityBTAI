using System;

namespace UnchordMetroidvania
{
    public class InverterNodeBT<T> : DecoratorNodeBT<T>
    {
        public InverterNodeBT(T instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            InvokeResult iResult = children[0]?.Invoke() ?? InvokeResult.Failure;

            if(iResult == InvokeResult.Success)
                return InvokeResult.Failure;
            else if(iResult == InvokeResult.Running)
                return InvokeResult.Running;
            else if(iResult == InvokeResult.Failure)
                return InvokeResult.Success;
            else
                throw new Exception("Unknown Exception.");
        }
    }
}