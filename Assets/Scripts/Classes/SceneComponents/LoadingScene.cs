using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unchord
{
    public class LoadingScene : SceneComponent
    {
        private static LoadingScene m_instance;

        private void Awake()
        {
            if(m_instance == null)
                m_instance = this;
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }

        private void Update()
        {
            Loading.cmdQueue.Execute(m_OnEndLoading);
        }

        private void OnDestroy()
        {
            if(m_instance == this)
                m_instance = null;
        }

        private void m_OnEndLoading()
        {
            Loading.EndLoading(gameObject.scene.name);
        }
    }
}