namespace Unchord
{
    public static partial class UnchordUtility
    {
        public static float Mid(float _a, float _b, float _c)
        {
            if(_a < _b)
            {
                if(_c < _a) return _a;
                else if(_c < _b) return _c;
                else return _b;
            }
            else if(_c < _b) return _b;
            else if(_c < _a) return _c;
            else return _a;
        }

        public static int Mid(int _a, int _b, int _c)
        {
            if(_a < _b)
            {
                if(_c < _a) return _a;
                else if(_c < _b) return _c;
                else return _b;
            }
            else if(_c < _b) return _b;
            else if(_c < _a) return _c;
            else return _a;
        }
    }
}