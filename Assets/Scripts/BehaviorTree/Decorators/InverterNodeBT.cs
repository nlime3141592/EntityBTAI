using System;

namespace UnchordMetroidvania
{
    public class InverterNodeBT<T> : DecoratorNodeBT<T>
    {
        internal InverterNodeBT(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            InvokeResult iResult = child.Invoke();

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