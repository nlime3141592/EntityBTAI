using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerJump : PlayerAbility
    {
        protected bool bJumpCanceled;

        public PlayerJump(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();

            bJumpCanceled = false;
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.axisInput.y < 0 && player.jumpDown)
            {
                fsm.Change(fsm.takeDown);
                return true;
            }
            else if(player.skill00 && fsm.attackOnAir.CanAttack())
            {
                fsm.Change(fsm.attackOnAir);
                return true;
            }
            else if(player.senseData.bOnCeil)
            {
                fsm.Change(fsm.freeFall);
                return true;
            }
            else if(!bJumpCanceled && player.jumpUp)
            {
                bJumpCanceled = true;
                p_OnJumpCanceled();
                return false;
            }
            else if(player.leftAirJumpCount > 0 && player.jumpDown)
            {
                fsm.Change(fsm.jumpOnAir);
                return true;
            }

            return false;
        }

        protected virtual void p_OnJumpCanceled()
        {

        }
    }
}