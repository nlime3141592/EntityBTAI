using System;

namespace Unchord
{
    public class MantisStateRegion3_002 : StateRegion3
    {
        protected override int OnQuarter2(Random _prng)
        {
            int prn = _prng.Next(5);

            if(prn < 1)                     return Mantis.c_st_WALK_FRONT;
            else                            return Mantis.c_st_BACK_SLICE;
        }

        protected override int OnRegion00(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 4)              return Mantis.c_st_UP_SLICE;
            else if(prn < 6)         return Mantis.c_st_COMBO_SHOUT;
            else if(prn < 9)         return Mantis.c_st_CHOP;
            else                            return Mantis.c_st_WALK_BACK;
        }

        protected override int OnRegion01(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 4)              return Mantis.c_st_CHOP;
            else if(prn < 6)         return Mantis.c_st_COMBO_SHOUT;
            else if(prn < 8)         return Mantis.c_st_UP_SLICE;
            else if(prn < 9)         return Mantis.c_st_WALK_BACK;
            else                            return Mantis.c_st_WALK_FRONT;
        }

        protected override int OnRegion02(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 3)              return Mantis.c_st_WALK_FRONT;
            else                            return Mantis.c_st_JUMP_CHOP;
        }

        protected override int OnRegion03(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 5)              return Mantis.c_st_UP_SLICE;
            else if(prn < 7)         return Mantis.c_st_COMBO_SHOUT;
            else if(prn < 8)         return Mantis.c_st_CHOP;
            else                            return Mantis.c_st_WALK_BACK;
        }

        protected override int OnRegion04(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 3)              return Mantis.c_st_CHOP;
            else if(prn < 5)         return Mantis.c_st_COMBO_SHOUT;
            else if(prn < 8)         return Mantis.c_st_UP_SLICE;
            else if(prn < 9)         return Mantis.c_st_WALK_BACK;
            else                            return Mantis.c_st_WALK_FRONT;
        }

        protected override int OnRegion05(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 3)              return Mantis.c_st_WALK_FRONT;
            else                            return Mantis.c_st_JUMP_CHOP;
        }

        protected override int OnRegion06(Random _prng)
        {
            int prn = _prng.Next(5);

            if(prn < 2)              return Mantis.c_st_UP_SLICE;
            else                            return Mantis.c_st_WALK_BACK;
        }

        protected override int OnRegion07(Random _prng)
        {
            int prn = _prng.Next(2);

            if(prn < 1)              return Mantis.c_st_UP_SLICE;
            else                            return Mantis.c_st_WALK_BACK;
        }

        protected override int OnRegion08(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 3)              return Mantis.c_st_WALK_FRONT;
            else                            return Mantis.c_st_JUMP_CHOP;
        }
    }
}