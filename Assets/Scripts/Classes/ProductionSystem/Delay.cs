using UnityEngine;

namespace Unchord
{
    public class Delay : ICommand
    {
        private float m_delay;

        private Delay(float _delay)
        {
            m_delay = _delay;
        }

        public static ICommand Get(float _delay)
        {
            return new Delay(_delay);
        }

        public void Execute(CommandQueueCallback _callbackOnEnd)
        {
            m_delay -= Time.deltaTime;

            if(m_delay <= 0)
                _callbackOnEnd();
        }
    }
}