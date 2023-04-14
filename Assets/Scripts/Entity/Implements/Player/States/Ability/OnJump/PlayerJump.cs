using UnityEngine;

namespace Unchord
{
    public abstract class PlayerJump : PlayerAbility
    {
        protected bool bJumpCanceled;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.MeltPositionX();
            instance.vm.MeltPositionY();

            bJumpCanceled = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!bJumpCanceled && instance.jumpUp)
            {
                bJumpCanceled = true;
                p_OnJumpCanceled();
            }
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.skill00)
            {
                if(instance.axis.y < 0)
                    return Player.c_st_TAKE_DOWN_001;
                else
                    return instance.stateNext_AttackOnAir;
            }
            else if(instance.senseData.datCeil.bOnHit)
                return Player.c_st_FREE_FALL;
            else if(instance.countLeft_JumpOnAir > 0 && instance.jumpDown)
                return Player.c_st_JUMP_ON_AIR;

            return MachineConstant.c_lt_PASS;
        }

        protected virtual void p_OnJumpCanceled()
        {

        }
    }
}