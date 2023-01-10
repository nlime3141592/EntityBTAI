using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public abstract class PlayerAttack : _PlayerAbility
    {
        public PlayerAttack(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

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
    }
}