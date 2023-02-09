using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public abstract class PlayerAttack : PlayerAbility
    {
        protected readonly List<EntityBase> targets;

        public PlayerAttack(Player _player)
        : base(_player)
        {
            targets = new List<EntityBase>(16);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.aController.bEndOfAnimation)
            {
                if(player.senseData.bOnFloor)
                    return PlayerFsm.c_st_IDLE_SHORT;
                else
                    return PlayerFsm.c_st_FREE_FALL;
            }

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            player.battleModule.ClearBattleState();
        }
    }
}