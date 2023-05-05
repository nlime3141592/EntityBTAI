using System;

namespace Unchord
{
    public abstract class StateRegion3
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

            m_events[0] = OnRegion00;
            m_events[1] = OnRegion01;
            m_events[2] = OnRegion02;
            m_events[3] = OnRegion03;
            m_events[4] = OnRegion04;
            m_events[5] = OnRegion05;
            m_events[6] = OnRegion06;
            m_events[7] = OnRegion07;
            m_events[8] = OnRegion08;

            m_events[9] = OnRegion09;
            m_events[10] = OnRegion10;
            m_events[11] = OnRegion11;
            m_events[12] = OnRegion12;
            m_events[13] = OnRegion13;
            m_events[14] = OnRegion14;
            m_events[15] = OnRegion15;
            m_events[16] = OnRegion16;
            m_events[17] = OnRegion17;

            m_events[18] = OnRegion18;
            m_events[19] = OnRegion19;
            m_events[20] = OnRegion20;
            m_events[21] = OnRegion21;
            m_events[22] = OnRegion22;
            m_events[23] = OnRegion23;
            m_events[24] = OnRegion24;
            m_events[25] = OnRegion25;
            m_events[26] = OnRegion26;

            m_events[27] = OnRegion27;
            m_events[28] = OnRegion28;
            m_events[29] = OnRegion29;
            m_events[30] = OnRegion30;
            m_events[31] = OnRegion31;
            m_events[32] = OnRegion32;
            m_events[33] = OnRegion33;
            m_events[34] = OnRegion34;
            m_events[35] = OnRegion35;
        }

        public int GetState(System.Random _prng, float ox, float oy, float px, float py, float lx, float ly, float rx1, float rx2, float ry1, float ry2)
        {
            int code = m_GetRegion(ox, oy, px, py, lx, ly, rx1, rx2, ry1, ry2);
            return m_events[code](_prng);
        }

        protected virtual int OnQuarter1(Random _prng) => MachineConstant.c_lt_PASS;
        protected virtual int OnQuarter2(Random _prng) => MachineConstant.c_lt_PASS;
        protected virtual int OnQuarter3(Random _prng) => MachineConstant.c_lt_PASS;
        protected virtual int OnQuarter4(Random _prng) => MachineConstant.c_lt_PASS;

        protected virtual int OnRegion00(Random _prng) => OnQuarter1(_prng);
        protected virtual int OnRegion01(Random _prng) => OnQuarter1(_prng);
        protected virtual int OnRegion02(Random _prng) => OnQuarter1(_prng);
        protected virtual int OnRegion03(Random _prng) => OnQuarter1(_prng);
        protected virtual int OnRegion04(Random _prng) => OnQuarter1(_prng);
        protected virtual int OnRegion05(Random _prng) => OnQuarter1(_prng);
        protected virtual int OnRegion06(Random _prng) => OnQuarter1(_prng);
        protected virtual int OnRegion07(Random _prng) => OnQuarter1(_prng);
        protected virtual int OnRegion08(Random _prng) => OnQuarter1(_prng);

        protected virtual int OnRegion09(Random _prng) => OnQuarter2(_prng);
        protected virtual int OnRegion10(Random _prng) => OnQuarter2(_prng);
        protected virtual int OnRegion11(Random _prng) => OnQuarter2(_prng);
        protected virtual int OnRegion12(Random _prng) => OnQuarter2(_prng);
        protected virtual int OnRegion13(Random _prng) => OnQuarter2(_prng);
        protected virtual int OnRegion14(Random _prng) => OnQuarter2(_prng);
        protected virtual int OnRegion15(Random _prng) => OnQuarter2(_prng);
        protected virtual int OnRegion16(Random _prng) => OnQuarter2(_prng);
        protected virtual int OnRegion17(Random _prng) => OnQuarter2(_prng);

        protected virtual int OnRegion18(Random _prng) => OnQuarter3(_prng);
        protected virtual int OnRegion19(Random _prng) => OnQuarter3(_prng);
        protected virtual int OnRegion20(Random _prng) => OnQuarter3(_prng);
        protected virtual int OnRegion21(Random _prng) => OnQuarter3(_prng);
        protected virtual int OnRegion22(Random _prng) => OnQuarter3(_prng);
        protected virtual int OnRegion23(Random _prng) => OnQuarter3(_prng);
        protected virtual int OnRegion24(Random _prng) => OnQuarter3(_prng);
        protected virtual int OnRegion25(Random _prng) => OnQuarter3(_prng);
        protected virtual int OnRegion26(Random _prng) => OnQuarter3(_prng);

        protected virtual int OnRegion27(Random _prng) => OnQuarter4(_prng);
        protected virtual int OnRegion28(Random _prng) => OnQuarter4(_prng);
        protected virtual int OnRegion29(Random _prng) => OnQuarter4(_prng);
        protected virtual int OnRegion30(Random _prng) => OnQuarter4(_prng);
        protected virtual int OnRegion31(Random _prng) => OnQuarter4(_prng);
        protected virtual int OnRegion32(Random _prng) => OnQuarter4(_prng);
        protected virtual int OnRegion33(Random _prng) => OnQuarter4(_prng);
        protected virtual int OnRegion34(Random _prng) => OnQuarter4(_prng);
        protected virtual int OnRegion35(Random _prng) => OnQuarter4(_prng);

        private int m_GetRegion(float ox, float oy, float px, float py, float lx, float ly, float rx1, float rx2, float ry1, float ry2)
        {
            int region = 0;
            float dx = px - ox;
            float dy = py - oy;

            if(lx < 0)
                dx = -dx;
            if(ly < 0)
                dy = -dy;

            if(dx < -rx2 || dx >= rx2)
                region += 2;
            else if(dx < -rx1 || dx >= rx1)
                region += 1;

            if(dy < -ry2 || dy >= ry2)
                region += 6;
            else if(dy < -ry1 || dy >= ry1)
                region += 3;

            if(dx < 0)
                region += 10;
            if(dy < 0)
                region += 20;

            return region;
        }
    }
}