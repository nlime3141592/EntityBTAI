using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public abstract class PlayerAttack : PlayerAbility, IBattleState
    {
        Entity IBattleState.attacker => instance;
        List<Entity> IBattleState.targets => this.targets;
        LTRB IBattleState.range => attackRange;
        int IBattleState.targetCount => this.targetCount;
        float IBattleState.baseDamage => this.baseDamage;

        protected readonly List<Entity> targets;
        public LTRB attackRange;
        public int targetCount;
        public float baseDamage;

        public PlayerAttack()
        {
            targets = new List<Entity>(16);
        }

        public override void OnMachineBegin(Player _instance, int _id)
        {
            base.OnMachineBegin(_instance, _id);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.battleModule.SetBattleState(this);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
            {
                if(instance.senseData.bOnFloor)
                    return Player.c_st_IDLE_SHORT;
                else
                    return Player.c_st_FREE_FALL;
            }

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.battleModule.ClearBattleState();
        }
    }
}