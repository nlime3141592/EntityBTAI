using UnityEngine;

namespace Unchord
{
    public class MainCamera : MonoBehaviour
    {
        public static MainCamera instance => m_camera;
        private static MainCamera m_camera;

        void Start()
        {
            if(m_camera == null)
            {
                m_camera = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }

        void Update()
        {
            
        }
    }
}