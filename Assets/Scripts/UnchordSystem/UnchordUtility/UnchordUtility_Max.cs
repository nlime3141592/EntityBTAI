using System;

namespace Unchord
{
    public static partial class UnchordUtility
    {
        public static float Max(float _a, float _b)
        {
            return _a > _b ? _a : _b;
        }

        public static int Max(int _a, int _b)
        {
            return _a > _b ? _a : _b;
        }
    }
}