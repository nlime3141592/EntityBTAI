using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _PlayerOnFloor : PlayerState
    {
        public _PlayerOnFloor(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            fsm.leftAirJumpCount = data.maxAirJumpCount;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.skill01 && fsm.abilitySword.CanAttack())
            {
                player.fsm.Change(fsm.abilitySword);
                return true;
            }
            else if(player.skill02 && fsm.abilityGun.CanAttack())
            {
                player.fsm.Change(fsm.abilityGun);
                return true;
            }
            else if(player.skill00 && fsm.attackOnFloor.CanAttack())
            {
                player.fsm.Change(fsm.attackOnFloor);
                return true;
            }
            else if(player.jumpDown)
            {
                player.fsm.Change(fsm.jumpOnFloor);
                return true;
            }
            else if(player.rushDown)
            {
                player.fsm.Change(fsm.roll);
                return true;
            }
            else if(!fsm.bOnFloor)
            {
                player.fsm.Change(fsm.freeFall);
                return true;
            }

            return false;
        }
    }
}