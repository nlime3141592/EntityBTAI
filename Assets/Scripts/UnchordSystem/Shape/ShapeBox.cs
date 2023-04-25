using System;

namespace Unchord
{
    [Serializable]
    public class ShapeBox : Shape
    {
        // 사각형의 점 위치 (global position)
        public float ltx { get; private set; }
        public float lty { get; private set; }
        public float lbx { get; private set; }
        public float lby { get; private set; }
        public float rtx { get; private set; }
        public float rty { get; private set; }
        public float rbx { get; private set; }
        public float rby { get; private set; }

        // 중심
        public float cx { get; private set; }
        public float cy { get; private set; }

        // 크기
        public float sx { get; private set; }
        public float sy { get; private set; }

        // 각도
        public float rad { get; private set; }
        public float deg { get; private set; }

        public float l = 0.5f;
        public float t = 0.5f;
        public float r = 0.5f;
        public float b = 0.5f;

        public override void Sync(Transform2 _transform)
        {
            float _ltx, _lty, _rtx, _rty, _lbx, _lby, _rbx, _rby;

            TransformManager2.GetGlobalPosition(out _ltx, out _lty, _transform, -l, t);
            TransformManager2.GetGlobalPosition(out _rtx, out _rty, _transform, r, t);
            TransformManager2.GetGlobalPosition(out _lbx, out _lby, _transform, -l, -b);
            TransformManager2.GetGlobalPosition(out _rbx, out _rby, _transform, r, -b);

            ltx = _ltx;
            lty = _lty;
            rtx = _rtx;
            rty = _rty;
            lbx = _lbx;
            lby = _lby;
            rbx = _rbx;
            rby = _rby;

            cx = 0.5f * (lbx + rtx);
            cy = 0.5f * (lby + rty);
            sx = rtx - ltx;
            sy = ltx - lbx;

            double rad = System.Math.Atan2(rtx - ltx, rty - lty);
            double deg = rad * 180 / System.Math.PI;

            rad = (float)rad;
            deg = (float)deg;
        }
    }
}