namespace UnchordMetroidvania
{
    public class PreDelayNodeBT<T_ConfigurationBT> : DecoratorNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        public int maxFrame { get; private set; }
        private int m_executedFrame;

        internal PreDelayNodeBT(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node, int frameCount)
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

            if(m_executedFrame < maxFrame)
            {
                return InvokeResult.RUNNING;
            }
            else if(m_executedFrame == maxFrame)
            {
                InvokeResult iResult = p_internalNode.Invoke();

                m_executedFrame = -1;

                if(iResult == InvokeResult.FAIL)
                    return InvokeResult.FAIL;
                else
                    return InvokeResult.SUCCESS;
            }
            else
            {
                m_executedFrame = -1;
                return InvokeResult.FAIL;
            }
        }
    }
}