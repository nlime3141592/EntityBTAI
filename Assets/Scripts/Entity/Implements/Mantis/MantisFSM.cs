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

        public _MantisIdle idle;
        public MantisWalkFront walkFront;
        public MantisWalkBack walkBack;

        public MantisFsm(Mantis _mantis)
        : base(_mantis)
        {
            MantisData data = _mantis.data;

            idle = new _MantisIdle(_mantis, c_st_IDLE, "Idle");
            walkFront = new MantisWalkFront(_mantis, c_st_WALK_FRONT, "WalkFront");
            walkBack = new MantisWalkBack(_mantis, c_st_WALK_BACK, "WalkBack");
        }

        public override bool OnUpdate()
        {
            return base.OnUpdate();
        }
    }
}