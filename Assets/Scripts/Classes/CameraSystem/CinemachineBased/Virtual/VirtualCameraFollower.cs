using UnityEngine;

namespace Unchord
{
    public sealed class VirtualCameraFollower : VirtualCameraComponentBase
    {
        public void Follow(Transform _followee, Vector2 _offset, float _degRotationZ)
        {
            baseComponent.m_Follow = _followee;
            base.SetPosition(_followee.position, _offset, _degRotationZ);
        }

        public void Unfollow(CommandQueueCallback _callbackOnEnd = null)
        {
            baseComponent.m_Follow = null;
            _callbackOnEnd?.Invoke();
        }

        public void Unfollow(Vector2 _postPosition, Vector2 _offset, float _degRotationZ)
        {
            baseComponent.m_Follow = null;
            base.SetPosition(_postPosition, _offset, _degRotationZ);
        }
    }
}