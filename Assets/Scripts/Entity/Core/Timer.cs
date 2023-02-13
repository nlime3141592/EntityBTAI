using UnityEngine;

namespace UnchordMetroidvania
{
    public class Timer
    {
        public float max
        {
            get => m_max;
            set => m_max = value < 0.0f ? 0.0f : value;
        }
        public float left => m_left;
        public bool canUpdate
        {
            get => m_bCanUpdate;
            set => m_bCanUpdate = value;
        }

        public bool bEndOfTimer => m_left <= 0.0f;

        private float m_max;
        private float m_left;
        private bool m_bCanUpdate;

        public Timer(float _max)
        {
            max = _max;

            Reset();
            m_bCanUpdate = true;
        }

        public void Reset()
        {
            m_left = m_max;
        }

        public void OnUpdate()
        {
            if(!m_bCanUpdate)
                return;
            else if(m_left > 0.0f)
                m_left -= Time.deltaTime;
            else if(m_left < 0.0f)
                m_left = 0.0f;
        }
    }
}