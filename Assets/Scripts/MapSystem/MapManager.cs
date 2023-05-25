using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unchord
{
    public static class MapManager
    {
        private static AsyncOperation s_m_operation;

        private static IEnumerator m_Process(AsyncOperation operation)
        {
            s_m_operation = operation;
            s_m_operation.allowSceneActivation = false;
            s_m_operation.completed += (op) =>
            {
                s_m_operation = null;
            };

            while(s_m_operation.progress < 0.9f)
                yield return null;

            s_m_operation.allowSceneActivation = true;
            yield return new WaitUntil(() => s_m_operation == null);
        }

        public static IEnumerator Close(int map)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(map);

            if(!scene.isLoaded || s_m_operation != null)
                yield break;

            yield return m_Process(SceneManager.UnloadSceneAsync(map));
        }

        public static IEnumerator Open(int map, Action onOpened)
        {
            Scene scene = SceneManager.GetSceneByBuildIndex(map);

            if(scene.isLoaded || s_m_operation != null)
                yield break;

            yield return m_Process(SceneManager.LoadSceneAsync(map, LoadSceneMode.Additive));
            onOpened();
        }
    }
}