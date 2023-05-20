using UnityEngine;

namespace Unchord
{
    public static partial class AreaSensorSystemExtension
    {
        public static Transform BindLocal(this Transform _transform, Transform2 _transform2)
        {
            return _transform
                .BindLocalPosition(_transform2)
                .BindLocalFlip(_transform2)
                .BindLocalScale(_transform2)
                .BindLocalRotation(_transform2);
        }

        public static Transform BindLocalPosition(this Transform _transform, Transform2 _transform2)
        {
            _transform2.lpx = _transform.localPosition.x;
            _transform2.lpy = _transform.localPosition.y;
            return _transform;
        }

        public static Transform BindGlobalPosition(this Transform _transform, Transform2 _transform2)
        {
            _transform2.lpx = _transform.position.x;
            _transform2.lpy = _transform.position.y;
            return _transform;
        }

        public static Transform BindLocalFlip(this Transform _transform, Transform2 _transform2)
        {
            _transform2.lfx = _transform.eulerAngles.y != 0;
            _transform2.lfy = _transform.eulerAngles.x != 0;
            return _transform;
        }

        public static Transform BindLocalScale(this Transform _transform, Transform2 _transform2)
        {
            _transform2.lsx = _transform.localScale.x;
            _transform2.lsy = _transform.localScale.y;
            return _transform;
        }

        public static Transform BindLocalRotation(this Transform _transform, Transform2 _transform2)
        {
            _transform2.ldeg = _transform.localEulerAngles.z;
            return _transform;
        }

        public static Transform BindGlobalRotation(this Transform _transform, Transform2 _transform2)
        {
            _transform2.ldeg = _transform.eulerAngles.z;
            return _transform;
        }
    }
}