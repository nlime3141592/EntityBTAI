using System;

namespace UnchordMetroidvania
{
    public class MantisFsm : UnchordFsm<Mantis>
    {
        public const int c_st_IDLE = 0;
        public const int c_st_WALK_FRONT = 1;
        public const int c_st_WALK_BACK = 2;
        public const int c_st_SHOUT = 3;
        public const int c_st_KNIFE_GRINDING = 4;
        public const int c_st_UP_SLICE = 5;
        public const int c_st_BACK_SLICE = 6;
        public const int c_st_CHOP = 7;
        public const int c_st_JUMP_CHOP = 8;
        public const int c_st_GROGGY = 9;

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

        public MantisFsm(Mantis _mantis)
        : base(_mantis)
        {
            MantisData data = _mantis.data;

            idle = new MantisIdle(_mantis, c_st_IDLE, "Idle");
            walkFront = new MantisWalkFront(_mantis, c_st_WALK_FRONT, "WalkFront");
            walkBack = new MantisWalkBack(_mantis, c_st_WALK_BACK, "WalkBack");
            shout = new MantisShout(_mantis, c_st_SHOUT, "Shout");
            knifeGrinding = new MantisKnifeGrinding(_mantis, c_st_KNIFE_GRINDING, "KnifeGrinding");
            upSlice = new MantisUpSlice(_mantis, c_st_UP_SLICE, "UpSlice");
            backSlice = new MantisBackSlice(_mantis, c_st_BACK_SLICE, "BackSlice");
            chop = new MantisChop(_mantis, c_st_CHOP, "Chop");
            jumpChop = new MantisJumpChop(_mantis, c_st_JUMP_CHOP, "JumpChop");
            groggy = new MantisGroggy(_mantis, c_st_GROGGY, "Groggy");
        }

        public override bool OnUpdate()
        {
            return base.OnUpdate();
        }
    }
}