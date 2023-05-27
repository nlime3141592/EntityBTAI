namespace UnchordMetroidvania
{
    public class RetryNodeBT<T> : DecoratorNodeBT<T>
    {
        public int tryCount
        {
            get => m_tryCount;
            set
            {
                // max(1, value)

                if(value < 1)
                    m_tryCount = 1;
                else
                    m_tryCount = value;
            }
        }

        private int m_tryCount = 1;
        private int m_triedCount = 0;

        public RetryNodeBT(T instance)
        : base(instance)
        {

        }

        public override void ResetNode()
        {
            m_triedCount = 0;

            base.ResetNode();
        }

        protected override InvokeResult p_Invoke()
        {
            InvokeResult iResult = children[0]?.Invoke() ?? InvokeResult.Failure;

            if(iResult == InvokeResult.Running)
            {
                return InvokeResult.Running;
            }
            else if(iResult == InvokeResult.Success)
            {
                ResetNode();
                return InvokeResult.Success;
            }
            else if(m_triedCount < tryCount - 1)
            {
                ++m_triedCount;
                return InvokeResult.Running;
            }
            else
            {
                ResetNode();
                return InvokeResult.Failure;
            }
        }
    }
}