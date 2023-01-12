namespace UnchordMetroidvania
{
    public class LoopNodeBT<T> : DecoratorNodeBT<T>
    {
        public int loopCount
        {
            get => m_loopCount;
            set
            {
                // max(1, value)

                if(value < 1)
                    m_loopCount = 1;
                else
                    m_loopCount = value;
            }
        }

        private int m_loopCount = 1;
        private int m_loopedCount = 0;

        public LoopNodeBT(T instance)
        : base(instance)
        {

        }

        public override void ResetNode()
        {
            m_loopedCount = 0;

            base.ResetNode();
        }

        protected override InvokeResult p_Invoke()
        {
            ++m_loopedCount;
            InvokeResult iResult = children[0]?.Invoke() ?? InvokeResult.Failure;

            if(iResult == InvokeResult.Failure)
            {
                ResetNode();
                return InvokeResult.Failure;
            }
            else
            {
                if(m_loopedCount < loopCount)
                    return InvokeResult.Running;

                ResetNode();
                return InvokeResult.Success;
            }
        }
    }
}