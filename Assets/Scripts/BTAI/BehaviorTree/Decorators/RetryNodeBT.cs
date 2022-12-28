namespace UnchordMetroidvania
{
    public class RetryNodeBT<T_ConfigurationBT> : DecoratorNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        public int tryCount { get; private set; }
        private int m_triedCount = 0;

        internal RetryNodeBT(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node, int count)
        : base(config, node)
        {
            SetTryCount(count);
            m_triedCount = 0;
        }

        public void SetTryCount(int count)
        {
            if(count < 0)
                tryCount = 0;
            else
                tryCount = count;
        }

        public override InvokeResult Invoke()
        {
            InvokeResult continuous = base.Invoke();

            if(continuous != InvokeResult.RUNNING)
                m_triedCount = 0;

            InvokeResult iResult = p_internalNode.Invoke();

            if(iResult == InvokeResult.RUNNING)
            {
                return InvokeResult.RUNNING;
            }
            else if(iResult == InvokeResult.SUCCESS)
            {
                m_triedCount = 0;
                return InvokeResult.SUCCESS;
            }
            else if(m_triedCount < tryCount - 1)
            {
                ++m_triedCount;
                return InvokeResult.RUNNING;
            }
            else
            {
                m_triedCount = 0;
                return InvokeResult.FAIL;
            }
        }
    }
}