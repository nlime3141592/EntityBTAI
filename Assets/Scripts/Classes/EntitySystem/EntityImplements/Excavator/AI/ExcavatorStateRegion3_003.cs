using System;

namespace Unchord
{
    [Serializable]
    public class ExcavatorStateRegion3_003 : StateRegion3
    {
        protected override int OnRegion00(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 6) return Excavator.c_st_STAMPING;
            else if(prn < 9) return Excavator.c_st_SHOCK_WAVE;
            else return Excavator.c_st_SHOOT_MISSILE;
        }

        protected override int OnRegion01(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 2) return Excavator.c_st_WALK;
            // else if(prn < 5) return Excavator.c_st_ANCHORING;
            else if(prn < 5) return Excavator.c_st_STAMPING;
            else if(prn < 8) return Excavator.c_st_SHOCK_WAVE;
            else return Excavator.c_st_SHOOT_MISSILE;
        }

        protected override int OnRegion02(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 2) return Excavator.c_st_WALK;
            // else if(prn < 4) return Excavator.c_st_ANCHORING;
            else if(prn < 4) return Excavator.c_st_STAMPING;
            else if(prn < 7) return Excavator.c_st_SHOCK_WAVE;
            else return Excavator.c_st_SHOOT_MISSILE;
        }

        protected override int OnRegion03(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 5) return Excavator.c_st_WALK;
            else if(prn < 8) return Excavator.c_st_SHOCK_WAVE;
            else return Excavator.c_st_SHOOT_MISSILE;
        }

        protected override int OnRegion04(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 2) return Excavator.c_st_WALK;
            // else if(prn < 5) return Excavator.c_st_ANCHORING;
            else if(prn < 5) return Excavator.c_st_STAMPING;
            else if(prn < 8) return Excavator.c_st_SHOCK_WAVE;
            else return Excavator.c_st_SHOOT_MISSILE;
        }

        protected override int OnRegion05(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 2) return Excavator.c_st_WALK;
            // else if(prn < 5) return Excavator.c_st_ANCHORING;
            else if(prn < 5) return Excavator.c_st_STAMPING;
            else if(prn < 7) return Excavator.c_st_SHOCK_WAVE;
            else return Excavator.c_st_SHOOT_MISSILE;
        }

        protected override int OnRegion06(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 5) return Excavator.c_st_WALK;
            else if(prn < 7) return Excavator.c_st_SHOCK_WAVE;
            else return Excavator.c_st_SHOOT_MISSILE;
        }

        protected override int OnRegion07(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 2) return Excavator.c_st_WALK;
            // else if(prn < 6) return Excavator.c_st_ANCHORING;
            else if(prn < 6) return Excavator.c_st_STAMPING;
            else if(prn < 8) return Excavator.c_st_SHOCK_WAVE;
            else return Excavator.c_st_SHOOT_MISSILE;
        }

        protected override int OnRegion08(Random _prng)
        {
            int prn = _prng.Next(10);

            if(prn < 2) return Excavator.c_st_WALK;
            // else if(prn < 6) return Excavator.c_st_ANCHORING;
            else if(prn < 6) return Excavator.c_st_STAMPING;
            else if(prn < 7) return Excavator.c_st_SHOCK_WAVE;
            else return Excavator.c_st_SHOOT_MISSILE;
        }
    }
}