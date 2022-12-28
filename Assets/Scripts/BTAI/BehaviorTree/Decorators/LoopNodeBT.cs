namespace UnchordMetroidvania
{
    public class LoopNodeBT<T_ConfigurationBT> : DecoratorNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        public int maxFrame { get; private set; }
        private int m_executedFrame;

        internal LoopNodeBT(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node, int frameCount)
        : base(config, node)
        {
            SetFrame(frameCount);
        }

        public void SetFrame(int frameCount)
        {
            if(frameCount < 0)
                maxFrame = 0;
            else
                maxFrame = frameCount;
        }

        public override InvokeResult Invoke()
        {
            InvokeResult continuous = base.Invoke();

            if(continuous != InvokeResult.RUNNING)
                m_executedFrame = -1;

            ++m_executedFrame;
            InvokeResult iResult = p_internalNode.Invoke();

            if(iResult == InvokeResult.FAIL)
            {
                m_executedFrame = -1;
                return InvokeResult.FAIL;
            }
            else if(m_executedFrame < maxFrame - 1)
            {
                return InvokeResult.RUNNING;
            }
            else
            {
                m_executedFrame = -1;
                return InvokeResult.SUCCESS;
            }
        }
    }
}