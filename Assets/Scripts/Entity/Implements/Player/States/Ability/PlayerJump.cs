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
            else if(player.bOnCeil)
            {
                player.fsm.Change(player.freeFall);
                return true;
            }
            else if(!bJumpCanceled && Input.GetKeyUp(KeyCode.Space))
            {
                bJumpCanceled = true;
                p_OnJumpCanceled();
                return false;
            }
            else if(player.leftAirJumpCount > 0 && Input.GetKeyDown(KeyCode.Space))
            {
                player.fsm.Change(player.jumpOnAir);
                return true;
            }

            return false;
        }

        protected virtual void p_OnJumpCanceled()
        {

        }
    }
}