using System;
using UnityEngine;

namespace Unchord
{
    public class PlayerAttackOnAir002 : PlayerAttackOnAirBase
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

            _instance.stateMap.Add(Player.c_st_ATTACK_ON_AIR_002, _id);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.stateNext_AttackOnAir = Player.c_st_ATTACK_ON_AIR_001;
        }
    }
}