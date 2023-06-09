using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unchord
{
    public static class Loading
    {
        public static readonly Fader fader = new Fader(0);
        public static readonly CommandQueue cmdQueue = new CommandQueue(8);

        public static bool bLoading { get; private set; } = false;

        private static AsyncOperation s_m_asyncLoader;
        private static AsyncOperation s_m_asyncUnloader;

        public static void StartLoading(string _loadingSceneName)
        {
            s_m_asyncLoader = SceneManager.LoadSceneAsync(_loadingSceneName, LoadSceneMode.Additive);
            s_m_asyncLoader.completed += (_op) =>
            {
                s_m_asyncLoader = null;
            };

            bLoading = true;
        }

        public static void EndLoading(string _loadingSceneName)
        {
            s_m_asyncUnloader = SceneManager.UnloadSceneAsync(_loadingSceneName);
            s_m_asyncUnloader.completed += (_op) =>
            {
                s_m_asyncUnloader = null;
                bLoading = false;
            };
        }

        public static ICommand GetDelay(float _time)
        {
            return new m_Delay(_time);
        }

        private class m_Delay : ICommand
        {
            private float m_delay;

            public m_Delay(float _delay)
            {
                m_delay = _delay;
            }

            public void Execute(CommandQueueCallback _callbackOnEnd)
            {
                m_delay -= Time.deltaTime;

                if(m_delay <= 0)
                    _callbackOnEnd();
            }
        }
    }
}