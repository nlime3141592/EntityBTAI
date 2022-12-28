namespace UnchordMetroidvania
{
    public class SequenceNodeBT<T_ConfigurationBT> : CompositeNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        internal SequenceNodeBT(T_ConfigurationBT config, int initCapacity)
        : base(config, -1, "Sequence", initCapacity)
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
                else if(iResult == InvokeResult.FAIL)
                {
                    p_childIndex = 0;
                    return iResult;
                }
            }

            p_childIndex = 0;
            return InvokeResult.SUCCESS;
        }
    }
}