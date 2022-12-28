namespace UnchordMetroidvania
{
    public class ParallelNodeBT<T_ConfigurationBT> : ControlNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        public int maxFrame { get; private set; }
        private int m_successCount = 0;
        private int m_failCount = 0;
        private bool mb_executed;

        internal ParallelNodeBT(T_ConfigurationBT config, int initCapacity)
        : base(config, -1, "Parallel", initCapacity)
        {
            m_successCount = 0;
            m_failCount = 0;
            mb_executed = false;
        }

        public override InvokeResult Invoke()
        {
            InvokeResult continuous = base.Invoke();

            if(continuous != InvokeResult.RUNNING)
            {
                m_successCount = 0;
                m_failCount = 0;
                mb_executed = false;
            }

            bool bPrevExecuted = mb_executed;
            mb_executed = true;
            NodeBT<T_ConfigurationBT> child;

            for(int i = 0; i < p_children.Count; ++i)
            {
                child = p_children[i];
                child.bSkipped &= bPrevExecuted;

                if(child.bSkipped)
                    continue;

                InvokeResult iResult = child.Invoke();

                if(iResult == InvokeResult.SUCCESS)
                {
                    child.bSkipped = true;
                    ++m_successCount;
                }
                else if(iResult == InvokeResult.FAIL)
                {
                    child.bSkipped = true;
                    ++m_failCount;
                }
                else
                {
                    continue;
                }

                if(m_successCount + m_failCount == p_children.Count)
                {
                    InvokeResult finalResult = InvokeResult.SUCCESS;
                    if(m_failCount > m_successCount)
                        finalResult = InvokeResult.FAIL;

                    m_successCount = 0;
                    m_failCount = 0;
                    mb_executed = false;

                    return finalResult;
                }
            }

            // when all nodes are skipped.
            return InvokeResult.RUNNING;
        }
    }
}