using System;

namespace UnchordMetroidvania
{
    public class ExcavatorFsm : EntityFsm<Excavator>
    {
        public const int c_st_SLEEP                     = 0;
        public const int c_st_WAKE_UP                   = 1;
        public const int c_st_IDLE                      = 2;
        public const int c_st_WALK                      = 3;
        public const int c_st_FREE_FALL                 = 4;
        public const int c_st_STAMPING                  = 5;
        public const int c_st_ANCHORING                 = 6;
        public const int c_st_SHOCK_WAVE                = 7; // 2페이즈부터
        public const int c_st_SHOOT_MISSILE             = 8; // 3페이즈부터
        public const int c_st_BREAK_GROUND              = 9;
        public const int c_st_BASIC_LANDING             = 10;
        public const int c_st_DIE                       = 11;
        public const int c_st_GROGGY                    = 12;

        public int mode = 1;

        public ExcavatorFsm(Excavator _instance, int _capacity)
        : base(_instance, _capacity)
        {
            ExcavatorData data = _instance.data;

            int idx = -1;

            this[++idx] = new ExcavatorSleep(_instance);
            this[++idx] = new ExcavatorWakeUp(_instance);
            this[++idx] = new ExcavatorIdle(_instance);
            this[++idx] = new ExcavatorWalkFront(_instance);
            this[++idx] = new ExcavatorFreeFall(_instance);
            this[++idx] = new ExcavatorStamping(_instance);
            this[++idx] = new ExcavatorAnchoring(_instance);
            this[++idx] = new ExcavatorShockWave(_instance);
            this[++idx] = new ExcavatorShootMissile(_instance);
            this[++idx] = new ExcavatorBreakFloor(_instance);
            this[++idx] = new ExcavatorLanding(_instance);
            this[++idx] = new ExcavatorDie(_instance);
            this[++idx] = new ExcavatorGroggy(_instance);

            mode = 1;
        }
    }
}