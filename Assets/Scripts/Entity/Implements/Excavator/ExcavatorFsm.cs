using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public const int c_st_SHOCK_WAVE                = 7;
        public const int c_st_SHOOT_MISSILE             = 8;
        public const int c_st_BREAK_GROUND              = 9;
        public const int c_st_BASIC_LANDING             = 10;

        public ExcavatorFsm(Excavator _instance, int _capacity)
        : base(_instance, _capacity)
        {

        }
    }
}