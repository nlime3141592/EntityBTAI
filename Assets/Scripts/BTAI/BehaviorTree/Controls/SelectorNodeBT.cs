namespace UnchordMetroidvania
{
    public class SelectorNodeBT<T_ConfigurationBT> : CompositeNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        internal SelectorNodeBT(T_ConfigurationBT config, int initCapacity)
        : base(config, -1, "Selector", initCapacity)
        {

        }

        public override InvokeResult Invoke()
        {
            base.Invoke();

            for(int i = p_childIndex; i < p_children.Count; ++i)
            {
                InvokeResult iResult = p_children[i].Invoke();

                if(iResult == InvokeResult.RUNNING)
                {
                    p_childIndex = i;
                    return iResult;
                }
                else if(iResult == InvokeResult.SUCCESS)
                {
                    p_childIndex = 0;
                    return iResult;
                }
            }

            p_childIndex = 0;
            return InvokeResult.FAIL;
        }
    }
}