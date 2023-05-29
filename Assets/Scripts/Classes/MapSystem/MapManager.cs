using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unchord
{
    public class MapManager : Manager<MapManager>
    {
        public IEnumerator Close(string _name)
        {
            Scene scene = SceneManager.GetSceneByName(_name);

            if(!scene.isLoaded)
                yield break;

            yield return m_Process(SceneManager.UnloadSceneAsync(_name));
        }

        public IEnumerator Open(string _name)
        {
            Scene scene = SceneManager.GetSceneByName(_name);

            if(scene.isLoaded)
                yield break;

            yield return m_Process(SceneManager.LoadSceneAsync(_name, LoadSceneMode.Additive));
        }

        private IEnumerator m_Process(AsyncOperation _operation)
        {
            _operation.allowSceneActivation = false;
            // _operation.completed += m_OnOperationCompleted;

            while(_operation.progress < 0.9f)
                yield return null;

            _operation.allowSceneActivation = true;
        }
    }
}