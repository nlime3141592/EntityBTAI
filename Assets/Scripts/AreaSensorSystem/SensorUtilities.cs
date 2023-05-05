using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public static class SensorUtilities
    {
        public static void Bind(Transform _transform, Transform2 _transform2)
        {
            _transform2.lpx = _transform.position.x;
            _transform2.lpy = _transform.position.y;
            _transform2.lfx = _transform.eulerAngles.y == 180;
            _transform2.lfy = _transform.eulerAngles.x == 180;
            _transform2.lsx = 1;
            _transform2.lsx = 1;
            _transform2.ldeg = _transform.eulerAngles.z;
        }
    }
}