using System;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public struct LTRB
    {
        public float left;
        public float top;
        public float right;
        public float bottom;

        public void Draw(Vector2 origin, bool flipX, bool flipY, Color color)
        {
            float ix = flipX ? -1 : 1;
            float iy = flipY ? -1 : 1;

            Vector2 beg = new Vector2(origin.x - left * ix, origin.y - bottom * iy);
            Vector2 end = new Vector2(origin.x + right * ix, origin.y + top * iy);
            Color clr = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawWireCube((beg + end) / 2, end - beg);
            Gizmos.color = clr;
        }
    }
}