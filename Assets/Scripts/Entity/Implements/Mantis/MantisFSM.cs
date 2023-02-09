using System;

namespace UnchordMetroidvania
{
    public class MantisFsm : EntityFsm<Mantis>
    {
        public const int c_st_IDLE                       = 0;
        public const int c_st_WALK_FRONT                 = 1;
        public const int c_st_WALK_BACK                  = 2;
        public const int c_st_SHOUT                      = 3;
        public const int c_st_KNIFE_GRINDING             = 4;
        public const int c_st_UP_SLICE                   = 5;
        public const int c_st_BACK_SLICE                 = 6;
        public const int c_st_CHOP                       = 7;
        public const int c_st_JUMP_CHOP                  = 8;
        public const int c_st_GROGGY                     = 9;
        public const int c_st_DIE                        = 10;

        public MantisIdle idle;
        public MantisWalkFront walkFront;
        public MantisWalkBack walkBack;
        public MantisShout shout;
        public MantisKnifeGrinding knifeGrinding;
        public MantisUpSlice upSlice;
        public MantisBackSlice backSlice;
        public MantisChop chop;
        public MantisJumpChop jumpChop;
        public MantisGroggy groggy;
        public MantisDie die;

        public int mode = 1;

        public MantisFsm(Mantis _mantis, int _capacity)
        : base(_mantis, _capacity)
        {
            MantisData data = _mantis.data;

            int idx = -1;

            this[++idx] = new MantisIdle(_mantis);
            this[++idx] = new MantisWalkFront(_mantis);
            this[++idx] = new MantisWalkBack(_mantis);
            this[++idx] = new MantisShout(_mantis);
            this[++idx] = new MantisKnifeGrinding(_mantis);
            this[++idx] = new MantisUpSlice(_mantis);
            this[++idx] = new MantisBackSlice(_mantis);
            this[++idx] = new MantisChop(_mantis);
            this[++idx] = new MantisJumpChop(_mantis);
            this[++idx] = new MantisGroggy(_mantis);
            this[++idx] = new MantisDie(_mantis);

            mode = 1;
        }
    }
}