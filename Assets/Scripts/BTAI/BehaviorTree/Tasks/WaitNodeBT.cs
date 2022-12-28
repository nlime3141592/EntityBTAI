namespace UnchordMetroidvania
{
    public class WaitNodeBT<T_ConfigurationBT> : TaskNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        public int maxFrame { get; private set; }
        private int m_executedFrame;

        internal WaitNodeBT(T_ConfigurationBT config, int frameCount)
        : base(config, -1, "Wait")
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

            if(m_executedFrame > -1 && m_executedFrame < maxFrame)
                return InvokeResult.RUNNING;
            else if(m_executedFrame == maxFrame)
            {
                m_executedFrame = -1;
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