using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerJumpOnFloor : PlayerJump
    {
        private float vy;

        public PlayerJumpOnFloor(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            vy = data.jumpOnFloorSpeed;
        }

        public override void OnFixedUpdate()
        {
            if(!player.aController.bBeginOfAction)
                return;

            base.OnFixedUpdate();

            float vx = player.bIsRun ? data.runSpeed : data.walkSpeed;
            float ix = player.axisInput.x;

            player.moveDir.x = ix;
            player.moveDir.y = 0;
            vx *= ix;

            if(vy <= 0)
            {
                p_bEndOfAbility = true;
                return;
            }

            player.vm.SetVelocityXY(vx, vy);
            vy -= (data.jumpOnFloorForce * Time.fixedDeltaTime);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.rushDown)
                return PlayerFsm.c_st_DASH;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        protected override void p_OnJumpCanceled()
        {
            base.p_OnJumpCanceled();
            vy /= 2;
        }
    }
}