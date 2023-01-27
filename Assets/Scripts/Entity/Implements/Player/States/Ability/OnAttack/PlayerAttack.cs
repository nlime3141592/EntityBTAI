using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public abstract class PlayerAttack : PlayerAbility
    {
        protected readonly List<EntityBase> targets;

        public PlayerAttack(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {
            targets = new List<EntityBase>(16);
        }

        public abstract bool CanAttack();

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.aController.bEndOfAnimation)
            {
                if(player.senseData.bOnFloor)
                    fsm.Change(fsm.idleShort);
                else
                    fsm.Change(fsm.freeFall);
                return true;
            }

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            player.battleModule.ClearBattleState();
        }
    }
}