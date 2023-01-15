using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public abstract class PlayerAttack : _PlayerAbility
    {
        protected readonly List<EntityBase> targets;

        public PlayerAttack(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {
            targets = new List<EntityBase>(20);
        }

        public abstract bool CanAttack();

        public override void OnStateBegin()
        {
            base.OnStateBegin();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(p_bEndOfAnimation)
            {
                if(player.bOnFloor)
                    player.fsm.Change(player.idleShort);
                else
                    player.fsm.Change(player.freeFall);
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