using UnityEngine;

namespace UnchordMetroidvania
{
    public class ParallaxObject : MonoBehaviour
    {
        [Range(0, 1)] public float parallaxEffectX = 0.1f;
        [Range(0, 1)] public float parallaxEffectY = 0.1f;
        public GameObject cam;

        public Vector3 startPosition;
        private Vector3 m_applyPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            float dx = cam.transform.position.x * parallaxEffectX;
            float dy = cam.transform.position.y * parallaxEffectY;
            m_applyPosition.Set(startPosition.x + dx, startPosition.y + dy, startPosition.z);
            transform.position = m_applyPosition;
        }
    }
}