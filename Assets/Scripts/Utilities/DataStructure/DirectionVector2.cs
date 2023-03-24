using System;

namespace Unchord
{
    [Serializable]
    public struct DirectionVector2
    {
        public int ix => (int)x;
        public int iy => (int)y;
        public float fx => (float)x;
        public float fy => (float)y;

        public Direction x;
        public Direction y;
    }
}