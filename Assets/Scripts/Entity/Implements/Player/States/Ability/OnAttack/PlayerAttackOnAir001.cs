using System;
using UnityEngine;

namespace Unchord
{
    public class PlayerAttackOnAir001 : PlayerAttackOnAirBase
    {
        public override int idConstant => Player.c_st_ATTACK_ON_AIR_001;

        public override void OnConstruct()
        {
            base.OnConstruct();

            base.baseDamage = 1.0f;
            base.speed_Hop = 20.0f;
            base.coyote = 2.0f;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.stateNext_AttackOnAir = Player.c_st_ATTACK_ON_AIR_002;
            --instance.countLeft_AttackOnAir;
        }

        public override bool CanTransit()
        {
            if(!base.CanTransit())
                return false;
            
            return instance.countLeft_AttackOnAir > 0;
        }
    }
}