using UnityEngine;
using Cinemachine;

namespace Unchord
{
    public abstract class VirtualCameraComponentBase : ExtendedComponent<CinemachineVirtualCamera>
    {
        public CinemachineFramingTransposer transposer => m_transposer;
        private CinemachineFramingTransposer m_transposer;

        protected override void OnValidate()
        {
            base.OnValidate();

            m_transposer = baseComponent.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        protected override void Awake()
        {
            base.Awake();

            m_transposer = baseComponent.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        public void SetPosition(Vector2 _position, Vector2 _offset, float _degRotationZ)
        {
            Vector3 posBase = new Vector3(_position.x, _position.y, baseComponent.transform.position.z);
            baseComponent.ForceCameraPosition(posBase + m_transposer.m_TrackedObjectOffset, Quaternion.Euler(0, 0, _degRotationZ));
        }
    }
}