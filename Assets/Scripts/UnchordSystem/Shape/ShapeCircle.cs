using System;

namespace Unchord
{
    [Serializable]
    public class ShapeCircle : Shape
    {
        public float cx { get; private set; }
        public float cy { get; private set; }
        public float r { get; private set; }

        public float radius = 0.5f;

        public override void Sync(Transform2 _transform)
        {
            float minScale = Utilities.Min<float>(_transform.gsx, _transform.gsy);

            cx = _transform.gpx;
            cy = _transform.gpy;
            r = radius * minScale;
        }
    }
}