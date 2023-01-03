namespace UnchordMetroidvania
{
    public class ParallelNodeBT<T> : ControlNodeBT<T>
    {
        private bool mb_executed;
        private int m_successCount;
        private int m_failureCount;
        private int m_skipCount;
        private bool[] m_bSkips;

        internal ParallelNodeBT(ConfigurationBT<T> config, int id, string name, int initCapacity)
        : base(config, id, name, initCapacity)
        {
            m_bSkips = new bool[initCapacity];
        }

        protected override InvokeResult p_Invoke()
        {
            bool bPrevExecuted = mb_executed;
            mb_executed = true;

            for(int i = 0; i < children.Length; ++i)
            {
                if(m_bSkips[i])
                    continue;

                InvokeResult iResult = children[i].Invoke();

                if(iResult == InvokeResult.Success)
                    m_OnSuccess(i);
                else if(iResult == InvokeResult.Failure)
                    m_OnFailure(i);
                else
                    continue;
            }

            if(m_skipCount == children.Length)
            {
                int sCnt = m_successCount;
                ResetNode();
                ResetChild();

                if(sCnt == children.Length)
                    return InvokeResult.Success;
                else
                    return InvokeResult.Failure;
            }
            else
                return InvokeResult.Running;
        }

        public override void ResetNode()
        {
            base.ResetNode();

            mb_executed = false;
            m_successCount = 0;
            m_failureCount = 0;
            m_skipCount = 0;

            for(int i = 0; i < m_bSkips.Length; ++i)
                m_bSkips[i] = false;
        }

        private void m_OnSuccess(int iChild)
        {
            m_bSkips[iChild] = true;
            ++m_successCount;
            ++m_skipCount;
        }

        private void m_OnFailure(int iChild)
        {
            m_bSkips[iChild] = true;
            ++m_failureCount;
            ++m_skipCount;
        }
    }
}