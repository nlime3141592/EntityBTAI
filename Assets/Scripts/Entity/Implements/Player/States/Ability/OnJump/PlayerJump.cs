using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerJump : PlayerAbility
    {
        protected bool bJumpCanceled;

        public PlayerJump(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();

            bJumpCanceled = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!bJumpCanceled && player.jumpUp)
            {
                bJumpCanceled = true;
                p_OnJumpCanceled();
            }
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.skill00)
            {
                if(player.axisInput.y < 0)
                    return PlayerFsm.c_st_TAKE_DOWN;
                else
                    return PlayerFsm.c_st_ATTACK_ON_AIR;
            }
            else if(player.senseData.bOnCeil)
                return PlayerFsm.c_st_FREE_FALL;
            else if(player.leftAirJumpCount > 0 && player.jumpDown)
                return PlayerFsm.c_st_JUMP_ON_AIR;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        protected virtual void p_OnJumpCanceled()
        {

        }
    }
}