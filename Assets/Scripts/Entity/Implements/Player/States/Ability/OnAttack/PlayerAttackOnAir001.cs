using System;
using UnityEngine;

namespace Unchord
{
    public class PlayerAttackOnAir001 : PlayerAttackOnAirBase
    {
        public override void OnConstruct()
        {
            base.OnConstruct();

            base.baseDamage = 1.0f;
            base.speed_Hop = 20.0f;
            base.coyote = 2.0f;
        }

        public override void OnMachineBegin(Player _instance, int _id)
        {
            base.OnMachineBegin(_instance, _id);

            _instance.stateMap.Add(Player.c_st_ATTACK_ON_AIR_001, _id);
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