namespace UnchordMetroidvania
{
    public class ParallelNodeBT<T> : ControlNodeBT<T>
    {
        private int m_successCount;
        private int m_failureCount;
        private int m_skipCount;
        private bool[] m_bSkips;

        public ParallelNodeBT(T instance, int capacity = 2)
        : base(instance, capacity)
        {
            m_bSkips = new bool[capacity];
        }

        public override void ResetNode()
        {
            int count = children?.Length ?? 0;
            for(int i = 0; i < count; ++i)
                children[i]?.ResetNode();

            m_successCount = 0;
            m_failureCount = 0;
            m_skipCount = 0;

            for(int i = 0; i < m_bSkips.Length; ++i)
                m_bSkips[i] = false;

            base.ResetNode();
        }

        protected override InvokeResult p_Invoke()
        {
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

                if(sCnt == children.Length)
                    return InvokeResult.Success;
                else
                    return InvokeResult.Failure;
            }
            else
                return InvokeResult.Running;
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