using UnityEngine;

namespace UnchordMetroidvania
{
    public class ParallaxObject : MonoBehaviour
    {
        public float xValue;
        public float yValue;

        public bool xFix;
        public bool yFix;
        private Vector3 m_pos;

        public bool useMinX;
        public float minX;
        public bool useMaxX;
        public float maxX;

        public bool useMinY;
        public float minY;
        public bool useMaxY;
        public float maxY;

        public void OnUpdate(Vector2 center, Vector2 position)
        {
            float dx = m_GetDelta(center.x, position.x, xValue, xFix, useMinX, minX, useMaxX, maxX);
            float dy = m_GetDelta(center.y, position.y, yValue, yFix, useMinY, minY, useMaxY, maxY);
            float dz = transform.localPosition.z;

            m_pos.x = dx;
            m_pos.y = dy;
            m_pos.z = dz;

            transform.localPosition = m_pos;
        }

        private float m_GetDelta(float c, float p, float pv, bool bFix, bool bMin, float vMin, bool bMax, float vMax)
        {
            float d = c - p;

            if(bFix)
                return d;
            else if(bMin && p < vMin)
                return (c - vMin) * pv + (vMin - p);
            else if(bMax && p > vMax)
                return (c - vMax) * pv + (vMax - p);
            else
                return d * pv;
        }
    }
}