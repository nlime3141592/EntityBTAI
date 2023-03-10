using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerJumpOnFloor : PlayerJump
    {
        private float vy;

        public PlayerJumpOnFloor(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

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

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.rushDown)
            {
                fsm.Change(fsm.dash);
                return true;
            }

            return false;
        }

        protected override void p_OnJumpCanceled()
        {
            base.p_OnJumpCanceled();
            vy /= 2;
        }
    }
}