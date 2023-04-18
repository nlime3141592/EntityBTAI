using System;

namespace Unchord
{
    public class StateRegion3
    {
        public RegionEvent this[int _index]
        {
            set
            {
                m_events[_index] = value;
            }
        }

        private RegionEvent[] m_events;

        public StateRegion3()
        {
            m_events = new RegionEvent[36];
        }

        public int GetState(System.Random _prng, float ox, float oy, float px, float py, float lx, float ly, float rx1, float rx2, float ry1, float ry2)
        {
            int code = m_GetRegion(ox, oy, px, py, lx, ly, rx1, rx2, ry1, ry2);
            return m_events[code](_prng);
        }

        private int m_GetRegion(float ox, float oy, float px, float py, float lx, float ly, float rx1, float rx2, float ry1, float ry2)
        {
            int region = 0;
            float dx = px - ox;
            float dy = py - oy;

            if(lx < 0)
                dx = -dx;
            if(ly < 0)
                dy = -dy;

            float absdx = dx;
            float absdy = dy;

            if(dx < 0)
            {
                dx = -dx;
                region += 9;
            }

            if(dy < 0)
            {
                dy = -dy;
                region += 18;
            }

            if(absdx >= rx2)
                region += 2;
            else if(absdx >= rx1)
                region += 1;

            if(absdy >= ry2)
                region += 6;
            else if(absdy >= ry1)
                region += 3;

            return region;
        }
    }
}