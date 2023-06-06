using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unchord
{
    public static class Map
    {
        public static AsyncOperation asyncLoader { get; private set; }
        public static AsyncOperation asyncUnloader { get; private set; }

        public static bool TryOpenMap(string _mapName)
        {
            if(asyncLoader == null)
            {
                asyncLoader = SceneManager.LoadSceneAsync(_mapName, LoadSceneMode.Additive);
                asyncLoader.allowSceneActivation = true;
                return false;
            }
            else if(asyncLoader.isDone)
            {
                asyncLoader = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TryCloseMap(string _mapName)
        {
            if(asyncUnloader == null)
            {
                asyncUnloader = SceneManager.UnloadSceneAsync(_mapName);
                asyncUnloader.allowSceneActivation = true;
                return false;
            }
            else if(asyncUnloader.isDone)
            {
                asyncUnloader = null;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}