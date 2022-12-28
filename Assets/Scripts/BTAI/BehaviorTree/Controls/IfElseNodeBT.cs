namespace UnchordMetroidvania
{
    public class IfElseNodeBT<T_ConfigurationBT> : ControlNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        internal IfElseNodeBT(T_ConfigurationBT config, int id, string name, int initCapacity)
        : base(config, id, name, initCapacity)
        {

        }

        public override InvokeResult Invoke()
        {
            if(p_children.Count != 2 && p_children.Count != 3)
                return InvokeResult.FAIL;

            InvokeResult continuous = base.Invoke();

            if(continuous != InvokeResult.RUNNING)
                p_childIndex = 0;

            InvokeResult iResult = InvokeResult.FAIL;

            if(p_childIndex == 0)
            {
                iResult = p_children[0].Invoke();

                if(iResult == InvokeResult.RUNNING)
                    return iResult;
                else if(iResult == InvokeResult.SUCCESS)
                    p_childIndex = 1;
                else if(iResult == InvokeResult.FAIL)
                    p_childIndex = 2;
            }

            iResult = InvokeResult.FAIL;

            if(p_childIndex > 0 && p_childIndex < p_children.Count && p_children[p_childIndex] != null)
                iResult = p_children[p_childIndex].Invoke();

            if(iResult == InvokeResult.RUNNING)
                return iResult;
            else
            {
                p_childIndex = 0;
                return iResult;
            }
        }
    }
}