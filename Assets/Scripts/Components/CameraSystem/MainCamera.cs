using UnityEngine;

namespace Unchord
{
    public class MainCamera : MonoBehaviour
    {
        public static MainCamera instance => m_camera;
        private static MainCamera m_camera;

        private void Start()
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

        private void Update()
        {
            CameraManager.instance.mainCameraPosition = transform.position;
        }
    }
}