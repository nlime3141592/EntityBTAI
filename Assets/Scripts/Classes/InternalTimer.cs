using System;

namespace Unchord
{
    public class InternalTimer
    {
        public event Action onEnd;
        public event Action<float, float> onSet;
        public event Action<float, float> onUpdate;

        private float m_leftTime;

        public void SetTimer(float _time)
        {
            float nowTime = m_leftTime;
            float nextTime = _time;

            m_leftTime = nextTime;
            onSet?.Invoke(nowTime, nextTime);
        }

        public void OnUpdate(float _dT)
        {
            if(m_leftTime == 0)
                return;

            float nowTime = m_leftTime;
            float nextTime = UnchordUtility.Max(0, m_leftTime - _dT);

            m_leftTime = nextTime;
            onUpdate?.Invoke(nowTime, nextTime);

            if(nextTime == 0 && nowTime > 0)
                onEnd();
        }
    }
}