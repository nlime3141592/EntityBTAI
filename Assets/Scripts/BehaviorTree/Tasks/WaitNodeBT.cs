namespace UnchordMetroidvania
{
    public class WaitNodeBT<T> : TaskNodeBT<T>
    {
        public int waitFrame { get; private set; }
        private int m_waitedFrame;

        internal WaitNodeBT(ConfigurationBT<T> config, int id, string name, int waitFrame)
        : base(config, id, name)
        {
            SetFrame(waitFrame);
            m_waitedFrame = -1;
        }

        public void SetFrame(int waitFrame)
        {
            if(waitFrame < 0)
                this.waitFrame = 0;
            else
                this.waitFrame = waitFrame;
        }

        protected override InvokeResult p_Invoke()
        {
            ++m_waitedFrame;

            if(m_waitedFrame > -1 && m_waitedFrame < waitFrame)
                return InvokeResult.Running;
            else if(m_waitedFrame == waitFrame)
            {
                ResetNode();
                return InvokeResult.Success;
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
            m_waitedFrame = -1;
        }
    }
}