using UnityEngine;

namespace Unchord
{
    public static class Loading
    {
        public static readonly Fader fader = new Fader(0);
        public static readonly CommandQueue cmdQueue = new CommandQueue(8);

        public static bool bLoading { get; private set; } = false;

        private static float s_m_delay;

        public static bool StartLoading()
        {
            bLoading = true;
            return true;
        }

        public static bool EndLoading()
        {
            bLoading = false;
            return true;
        }

        public static bool ClearDelay()
        {
            s_m_delay = 0;
            return true;
        }

        public static Command GetUpdateDelayCommand(float _time)
        {
            return () =>
            {
                float next = s_m_delay + Time.deltaTime;
                s_m_delay = next;
                return next >= _time;
            };
        }
    }
}