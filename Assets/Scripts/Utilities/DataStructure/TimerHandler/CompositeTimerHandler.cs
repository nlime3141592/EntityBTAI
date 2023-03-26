using System;
using System.Collections.Generic;

namespace Unchord
{
    public class CompositeTimerHandler : TimerHandlerBase
    {
        private List<TimerHandlerBase> m_timers;

        public CompositeTimerHandler(int _capacity = 1)
        {
            m_timers = new List<TimerHandlerBase>(Utilities.Max<int>(1, _capacity));
        }

        public void Add(TimerHandlerBase _timer)
        {
            if(!m_timers.Contains(_timer))
                m_timers.Add(_timer);
        }

        public bool Remove(TimerHandlerBase _timer)
        {
            return m_timers.Remove(_timer);
        }

        public override void OnUpdate(float _deltaTime)
        {
            int count = m_timers.Count;

            for(int i = 0; i < count; ++i)
                m_timers[i].OnUpdate(_deltaTime);
        }
    }
}