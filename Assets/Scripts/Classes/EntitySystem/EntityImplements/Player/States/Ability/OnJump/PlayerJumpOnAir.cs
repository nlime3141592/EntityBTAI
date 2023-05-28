using UnityEngine;

namespace Unchord
{
    public class PlayerJumpOnAir : PlayerJump
    {
        private float vy;

        public override int idConstant => Player.c_st_JUMP_ON_AIR;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            --(instance.countLeft_JumpOnAir);
            vy = instance.speed_JumpOnAir;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = instance.bIsRun ? instance.speed_Run : instance.speed_Walk;
            float ix = instance.iManager.ix;

            instance.moveDir.x = ix;
            instance.moveDir.y = 0;
            vx *= ix;

            instance.vm.SetVelocityXY(vx, vy);
            if(vy > 0)
                vy -= (instance.force_JumpOnAir * Time.fixedDeltaTime);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.iManager.rushDown)
                return Player.c_st_DASH;
            else if(vy <= 0)
                return Player.c_st_FREE_FALL;

            return MachineConstant.c_lt_PASS;
        }

        protected override void p_OnJumpCanceled()
        {
            base.p_OnJumpCanceled();
            vy /= 2;
        }
    }
}