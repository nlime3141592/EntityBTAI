using System;

namespace Unchord
{
    public class ExcavatorStateRegion3_001 : StateRegion3
    {
        protected override int OnRegion00(Random _prng)
        {
            return Excavator.c_st_STAMPING;
        }

        protected override int OnRegion01(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 3) return Excavator.c_st_WALK;
            else return Excavator.c_st_ANCHORING;
        }

        protected override int OnRegion02(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 5) return Excavator.c_st_WALK;
            else return Excavator.c_st_ANCHORING;
        }

        protected override int OnRegion03(Random _prng)
        {
            return Excavator.c_st_STAMPING;
        }

        protected override int OnRegion04(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 2) return Excavator.c_st_WALK;
            else return Excavator.c_st_ANCHORING;
        }

        protected override int OnRegion05(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 4) return Excavator.c_st_WALK;
            else return Excavator.c_st_ANCHORING;
        }

        protected override int OnRegion06(Random _prng)
        {
            return Excavator.c_st_STAMPING;
        }

        protected override int OnRegion07(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 2) return Excavator.c_st_WALK;
            else return Excavator.c_st_ANCHORING;
        }

        protected override int OnRegion08(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 3) return Excavator.c_st_WALK;
            else return Excavator.c_st_ANCHORING;
        }
    }
}