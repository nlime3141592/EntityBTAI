using UnityEngine;
using Cinemachine;

namespace UnchordMetroidvania
{
    public class CameraTraceModule : MonoBehaviour
    {
        public float posZ = -10;

        private CinemachineVirtualCamera vCam;

        private void Start()
        {
            vCam = GetComponent<CinemachineVirtualCamera>();
        }

        public void Alloc(Transform target)
        {
            vCam.m_Follow = target;
        }

        public void Dealloc(Vector2 position)
        {
            vCam.m_Follow = null;
            // vCam.m_LookAt = null;
            vCam.transform.position = new Vector3(position.x, position.y, posZ);
        }
    }
}