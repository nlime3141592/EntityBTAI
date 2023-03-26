using UnityEngine;

namespace Unchord
{
    public class PlayerJumpOnWallFront : PlayerJump
    {
        private float vy;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.bFixedLookDirByAxis.x = true;
            vy = instance.speed_JumpOnWall_Y;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = instance.speed_JumpOnWall_X;
            float ix = -instance.lookDir.fx;

            if(bJumpCanceled)
                ix = instance.axis.x;

            instance.moveDir.x = ix;
            instance.moveDir.y = 0;
            vx *= ix;

            instance.vm.SetVelocityXY(vx, vy);
            if(vy > 0)
                vy -= (instance.force_JumpOnWall * Time.fixedDeltaTime);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(bJumpCanceled && instance.rushDown)
                return Player.c_st_DASH;
            else if(vy <= 0)
                return Player.c_st_FREE_FALL;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.bFixedLookDirByAxis.x = false;
        }

        protected override void p_OnJumpCanceled()
        {
            base.p_OnJumpCanceled();
            instance.bFixedLookDirByAxis.x = false;
            vy /= 2;
        }
    }
}