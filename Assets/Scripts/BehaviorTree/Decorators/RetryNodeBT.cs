namespace UnchordMetroidvania
{
    public class RetryNodeBT<T> : DecoratorNodeBT<T>
    {
        public int tryCount { get; private set; }
        private int m_triedCount = 0;

        internal RetryNodeBT(ConfigurationBT<T> config, int id, string name, int tryCount)
        : base(config, id, name)
        {
            SetTryCount(tryCount);
        }

        public void SetTryCount(int tryCount)
        {
            if(tryCount < 0)
                this.tryCount = 0;
            else
                this.tryCount = tryCount;
        }

        protected override InvokeResult p_Invoke()
        {
            InvokeResult iResult = child.Invoke();

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

        public override void ResetNode()
        {
            base.ResetNode();

            m_triedCount = 0;
        }
    }
}