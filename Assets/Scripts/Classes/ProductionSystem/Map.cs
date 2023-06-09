using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unchord
{
    public static class Map
    {
        private abstract class m_MapAsyncOperator : ICommand
        {
            protected string mapName;
            protected AsyncOperation asyncOperation;

            public m_MapAsyncOperator(string _mapName)
            {
                mapName = _mapName;
            }

            public abstract void Execute(CommandQueueCallback _callbackOnEnd);
        }

        private sealed class m_MapOpenAsyncOperator : m_MapAsyncOperator
        {
            public m_MapOpenAsyncOperator(string _mapName)
            : base(_mapName)
            {

            }

            public override void Execute(CommandQueueCallback _callbackOnEnd)
            {
                if(base.asyncOperation == null)
                    base.asyncOperation = SceneManager.LoadSceneAsync(mapName, LoadSceneMode.Additive);

                if(base.asyncOperation.isDone)
                    _callbackOnEnd();
            }
        }

        private sealed class m_MapCloseAsyncOperator : m_MapAsyncOperator
        {
            public m_MapCloseAsyncOperator(string _mapName)
            : base(_mapName)
            {

            }

            public override void Execute(CommandQueueCallback _callbackOnEnd)
            {
                if(base.asyncOperation == null)
                    base.asyncOperation = SceneManager.UnloadSceneAsync(mapName);

                if(base.asyncOperation.isDone)
                    _callbackOnEnd();
            }
        }

        public static ICommand Open(string _mapName)
        {
            return new m_MapOpenAsyncOperator(_mapName);
        }

        public static ICommand Close(string _mapName)
        {
            return new m_MapCloseAsyncOperator(_mapName);
        }
    }
}