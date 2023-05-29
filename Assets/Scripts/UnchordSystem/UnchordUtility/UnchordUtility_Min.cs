using System;

namespace Unchord
{
    public static partial class UnchordUtility
    {
        public static float Min(float _a, float _b)
        {
            return _a < _b ? _a : _b;
        }

        public static int Min(int _a, int _b)
        {
            return _a < _b ? _a : _b;
        }
    }
}