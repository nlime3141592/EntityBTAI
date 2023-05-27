using System;

namespace UnchordMetroidvania
{
    public class WaitNodeBT<T> : TaskNodeBT<T>
    {
        public int waitCount
        {
            get => m_waitCount;
            set
            {
                // max(1, value)

                if(m_waitCount == value)
                    return;
                else if(value < 1)
                    m_waitCount = 1;
                else
                    m_waitCount = value;

                m_leftCount = m_GetRandomCount();
            }
        }

        public int cntDeviation
        {
            get => m_cntDeviation;
            set
            {
                // max(0, value)

                if(m_cntDeviation == value)
                    return;
                else if(value < 0)
                    m_cntDeviation = 0;
                else
                    m_cntDeviation = value;

                m_leftCount = m_GetRandomCount();
            }
        }

        private int m_waitCount = 1;
        private int m_cntDeviation = 0;
        private int m_leftCount = 0;
        private Random m_prng;

        public WaitNodeBT(T instance)
        : base(instance)
        {
            m_prng = new Random();
            m_leftCount = m_GetRandomCount();
        }

        public override void ResetNode()
        {
            base.ResetNode();
            m_leftCount = m_GetRandomCount();
        }

        protected override InvokeResult p_Invoke()
        {
            --m_leftCount;

            if(m_leftCount >= 0)
                return InvokeResult.Running;
            else if(m_leftCount < 0)
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

        private int m_GetRandomCount()
        {
            int min = m_waitCount - m_cntDeviation;
            int max = m_waitCount + m_cntDeviation;
            int value = m_prng.Next(min, max + 1);
            return value;
        }
    }
}