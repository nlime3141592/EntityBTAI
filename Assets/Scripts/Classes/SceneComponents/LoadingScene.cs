using UnityEngine;

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
            if(Loading.cmdQueue.Count > 0)
                Loading.cmdQueue.Execute();
            else
                Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            if(m_instance == this)
                m_instance = null;
        }
    }
}