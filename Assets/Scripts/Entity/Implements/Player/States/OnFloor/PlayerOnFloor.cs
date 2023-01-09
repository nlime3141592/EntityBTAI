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
            player.leftAirJumpCount = data.maxAirJumpCount;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.skill01 && player.abilitySword.CanAttack())
            {
                player.fsm.Change(player.abilitySword);
                return true;
            }
            else if(player.skill02 && player.abilityGun.CanAttack())
            {
                player.fsm.Change(player.abilityGun);
                return true;
            }
            else if(player.skill00 && player.attackOnFloor.CanAttack())
            {
                player.fsm.Change(player.attackOnFloor);
                return true;
            }
            else if(player.jumpDown)
            {
                player.fsm.Change(player.jumpOnFloor);
                return true;
            }
            else if(player.rushDown)
            {
                player.fsm.Change(player.roll);
                return true;
            }
            else if(!player.bOnFloor)
            {
                player.fsm.Change(player.freeFall);
                return true;
            }

            return false;
        }
    }
}