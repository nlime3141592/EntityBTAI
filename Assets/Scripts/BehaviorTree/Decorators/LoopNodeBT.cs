namespace UnchordMetroidvania
{
    public class LoopNodeBT<T> : DecoratorNodeBT<T>
    {
        public int loopFrame { get; private set; }
        private int m_loopedFrame;

        internal LoopNodeBT(ConfigurationBT<T> config, int id, string name, int loopFrame)
        : base(config, id, name)
        {
            SetFrame(loopFrame);
        }

        public void SetFrame(int loopFrame)
        {
            if(loopFrame < 0)
                this.loopFrame = 0;
            else
                this.loopFrame = loopFrame;
        }

        protected override InvokeResult p_Invoke()
        {
            ++m_loopedFrame;
            InvokeResult iResult = child.Invoke();

            if(iResult == InvokeResult.Failure)
            {
                ResetNode();
                return InvokeResult.Failure;
            }
            else
            {
                if(m_loopedFrame < loopFrame)
                    return InvokeResult.Running;

                ResetNode();
                return InvokeResult.Success;
            }
        }

        public override void ResetNode()
        {
            base.ResetNode();

            m_loopedFrame = 0;
        }
    }
}