using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerOnFloor : PlayerState
    {
        public PlayerOnFloor(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            player.leftAirJumpCount = player.data.maxAirJumpCount;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.skill00 && fsm.attackOnFloor.CanAttack())
            {
                fsm.Change(fsm.attackOnFloor);
                return true;
            }
            else if(player.skill01 && fsm.abilitySword.CanAttack())
            {
                fsm.Change(fsm.abilitySword);
                return true;
            }
            else if(player.skill02 && fsm.abilityGun.CanAttack())
            {
                fsm.Change(fsm.abilityGun);
                return true;
            }
            else if(player.parryingDown)
            {
                fsm.Change(fsm.basicParrying);
                return true;
            }
            else if(player.jumpDown)
            {
                fsm.Change(fsm.jumpOnFloor);
                return true;
            }
            else if(player.rushDown)
            {
                fsm.Change(fsm.roll);
                return true;
            }
            else if(!player.senseData.bOnFloor)
            {
                fsm.Change(fsm.freeFall);
                return true;
            }

            return false;
        }
    }
}