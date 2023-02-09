using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerJumpOnWallFront : PlayerJump
    {
        private float vy;

        public PlayerJumpOnWallFront(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.bFixLookDirX = true;
            vy = data.jumpOnWallSpeedY;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = data.jumpOnWallSpeedX;
            float ix = -player.lookDir.x;

            if(bJumpCanceled)
                ix = player.axisInput.x;

            player.moveDir.x = ix;
            player.moveDir.y = 0;
            vx *= ix;

            if(vy <= 0)
            {
                p_bEndOfAbility = true;
                return;
            }

            player.vm.SetVelocityXY(vx, vy);
            vy -= (data.jumpOnWallForce * Time.fixedDeltaTime);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(bJumpCanceled && player.rushDown)
                return PlayerFsm.c_st_DASH;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            player.bFixLookDirX = false;
        }

        protected override void p_OnJumpCanceled()
        {
            base.p_OnJumpCanceled();
            player.bFixLookDirX = false;
            vy /= 2;
        }
    }
}