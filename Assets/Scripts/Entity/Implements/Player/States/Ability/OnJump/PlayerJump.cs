using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _PlayerJump : _PlayerAbility
    {
        protected bool bJumpCanceled;

        public _PlayerJump(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

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
            else if(fsm.bOnCeil)
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
            else if(fsm.leftAirJumpCount > 0 && player.jumpDown)
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