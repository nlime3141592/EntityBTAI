namespace Unchord
{
    public static class TransformManager2
    {
        public static void SyncBasis(Transform2 _parent, Transform2 _child)
        {
            _child.gfx = _parent.gfx ^ _child.lfx;
            _child.gfy = _parent.gfy ^ _child.lfy;

            float ba = _parent.ba;
            float bb = _parent.bb;
            float bc = _parent.bc;
            float bd = _parent.bd;

            float gffx = s_m_GetFlipFloat(_child.gfx);
            float gffy = s_m_GetFlipFloat(_child.gfy);
            float lffx = s_m_GetFlipFloat(_child.lfx);
            float lffy = s_m_GetFlipFloat(_child.lfy);
            float rad = gffx * gffy * _child.ldeg * (float)System.Math.PI / 180;
            float cos = (float)System.Math.Cos(rad);
            float sin = (float)System.Math.Sin(rad);

            if(_child.lsx < 0) _child.lsx = 0;
            if(_child.lsy < 0) _child.lsy = 0;

            float xScale = lffx * _child.lsx;
            float yScale = lffy * _child.lsy;

            GetGlobalPosition(out _child.gpx, out _child.gpy, _parent, _child.lpx, _child.lpy);
            _child.gsx = _parent.gsx * _child.lsx;
            _child.gsy = _parent.gsy * _child.lsy;
            _child.ba = xScale * (ba * cos - bc * sin);
            _child.bb = yScale * (bb * cos - bd * sin);
            _child.bc = xScale * (ba * sin + bc * cos);
            _child.bd = yScale * (bb * sin + bd * cos);
        }

        public static void SyncRootBasis(Transform2 _root)
        {
            _root.gfx = _root.lfx;
            _root.gfy = _root.lfy;

            float ffx = s_m_GetFlipFloat(_root.gfx);
            float ffy = s_m_GetFlipFloat(_root.gfy);
            float rad = _root.ldeg * (float)System.Math.PI / 180; // degree to radian
            float cos = (float)System.Math.Cos(rad);
            float sin = (float)System.Math.Sin(rad);

            if(_root.lsx < 0) _root.lsx = 0;
            if(_root.lsy < 0) _root.lsy = 0;

            _root.gpx = _root.lpx;
            _root.gpy = _root.lpy;
            _root.gsx = _root.lsx;
            _root.gsy = _root.lsy;
            _root.ba = ffx * _root.lsx * cos;
            _root.bb = -ffx * _root.lsy * sin;
            _root.bc = ffy * _root.lsx * sin;
            _root.bd = ffy * _root.lsy * cos;
        }

        public static void GetGlobalPosition(out float _gpx, out float _gpy, Transform2 _coordinate, float _lpx, float _lpy)
        {
            _gpx = _coordinate.gpx + _coordinate.ba * _lpx + _coordinate.bb * _lpy;
            _gpy = _coordinate.gpy + _coordinate.bc * _lpx + _coordinate.bd * _lpy;
        }

        public static void GetLocalPosition(out float _lpx, out float _lpy, Transform2 _coordinate, float _gpx, float _gpy)
        {
            float determinant = _coordinate.ba * _coordinate.bd - _coordinate.bb * _coordinate.bc;
            float dgpx = _gpx - _coordinate.gpx;
            float dgpy = _gpy - _coordinate.gpy;

            _lpx = _coordinate.gsx == 0 ? 0 : (_coordinate.bd * dgpx - _coordinate.bb * dgpy) / determinant;
            _lpy = _coordinate.gsy == 0 ? 0 : (_coordinate.ba * dgpy - _coordinate.bc * dgpx) / determinant;
        }

        private static float s_m_GetFlipFloat(bool _bFliped)
        {
            return _bFliped ? -1 : 1;
        }
    }
}