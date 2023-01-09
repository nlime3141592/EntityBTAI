using System;

namespace UnchordMetroidvania
{
    public class WaitNodeBT<T> : TaskNodeBT<T>
    {
        public int waitFrame;
        public int frameDifference = 0;
        private int m_differenceFrame;
        private int m_leftFrame;
        private Random m_prng;

        internal WaitNodeBT(ConfigurationBT<T> config, int id, string name, int waitFrame)
        : base(config, id, name)
        {
            this.waitFrame = waitFrame;
            m_prng = new Random();

            ResetNode();
        }

        protected override InvokeResult p_Invoke()
        {
            --m_leftFrame;

            if(m_leftFrame > 0)
                return InvokeResult.Running;
            else if(m_leftFrame <= 0)
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

            if(frameDifference > 0)
                m_leftFrame = (int)m_prng.RangeNextDouble((double)waitFrame, (double)frameDifference);
            else if(frameDifference < 0)
                m_leftFrame = (int)m_prng.RangeNextDouble((double)waitFrame, (double)-frameDifference);
            else
                m_leftFrame = waitFrame;
        }
    }
}