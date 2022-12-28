namespace UnchordMetroidvania
{
    public class IfElseReactiveNodeBT<T_ConfigurationBT> : ControlNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        internal IfElseReactiveNodeBT(T_ConfigurationBT config, int id, string name, int initCapacity)
        : base(config, id, name, initCapacity)
        {

        }

        public override InvokeResult Invoke()
        {
            if(p_children.Count != 2 && p_children.Count != 3)
                return InvokeResult.FAIL;

            p_childIndex = 0;
            InvokeResult iResult = p_children[0].Invoke();

            if(iResult == InvokeResult.FAIL)
                p_childIndex = 2;
            else
                p_childIndex = 1;

            iResult = InvokeResult.FAIL;

            if(p_childIndex > 0 && p_childIndex < p_children.Count && p_children[p_childIndex] != null)
                iResult = p_children[p_childIndex].Invoke();

            return iResult;
        }
    }
}