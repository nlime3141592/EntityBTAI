using System;
using System.Collections.Generic;

namespace Unchord
{
    public class TimerHandler : TimerHandlerBase
    {
        public event Action onEndOfTimer;
        private float m_leftTime;

        public void SetTimer(float _time)
        {
            m_leftTime = _time;
        }

        public override void OnUpdate(float _deltaTime)
        {
            if(m_leftTime > 0)
            {
                m_leftTime -= _deltaTime;

                if(m_leftTime <= 0)
                    onEndOfTimer();
            }
        }
    }
}