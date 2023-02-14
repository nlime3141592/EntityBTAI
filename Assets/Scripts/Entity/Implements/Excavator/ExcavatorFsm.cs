using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorFsm : EntityFsm<Excavator>
    {
        public const int c_st_IDLE = 0;
        public const int c_st_WALK = 1;
        public const int c_st_FREE_FALL = 2;
        public const int c_st_STAMPING = 3;
        public const int c_st_ANCHORING = 4;
        public const int c_st_SHOCK_WAVE = 5;
        public const int c_st_SHOOT_MISSILE = 6;
        public const int c_st_BREAK_GROUND = 7;
        public const int c_st_BASIC_LANDING = 8;

        public ExcavatorFsm(Excavator _instance, int _capacity)
        : base(_instance, _capacity)
        {

        }
    }
}