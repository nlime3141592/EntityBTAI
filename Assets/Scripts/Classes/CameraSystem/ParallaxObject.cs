using UnityEngine;

namespace Unchord
{
    [DisallowMultipleComponent]
    public class ParallaxObject : MonoBehaviour
    {
        public float parallaxEffectX = 0.0f;
        public float parallaxEffectY = 0.0f;

        private Vector3 m_initPosition;
        private Vector3 m_applyPosition;

        private void Awake()
        {
            m_initPosition = transform.position;
        }

        private void Update()
        {
            Vector3 mainPos = CameraManager.instance.mainCameraPosition;

            m_applyPosition.Set(
                m_initPosition.x + (mainPos.x - m_initPosition.x) * parallaxEffectX,
                m_initPosition.y + (mainPos.y - m_initPosition.y) * parallaxEffectY,
                m_initPosition.z
            );

            transform.position = m_applyPosition;
        }
    }
}