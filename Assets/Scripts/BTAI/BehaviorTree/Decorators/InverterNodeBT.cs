using System;

namespace UnchordMetroidvania
{
    public class InverterNodeBT<T_ConfigurationBT> : DecoratorNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        internal InverterNodeBT(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node)
        : base(config, node)
        {

        }

        public override InvokeResult Invoke()
        {
            InvokeResult iResult = p_internalNode.Invoke();

            if(iResult == InvokeResult.SUCCESS)
                return InvokeResult.FAIL;
            else if(iResult == InvokeResult.RUNNING)
                return InvokeResult.RUNNING;
            else if(iResult == InvokeResult.FAIL)
                return InvokeResult.SUCCESS;
            else
                throw new Exception("Argument Exception.");
        }
    }
}