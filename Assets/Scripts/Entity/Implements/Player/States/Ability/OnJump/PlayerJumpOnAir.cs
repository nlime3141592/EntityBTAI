using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerJumpOnAir : PlayerJump
    {
        private float vy;

        public PlayerJumpOnAir(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            --(player.leftAirJumpCount);
            vy = data.jumpOnAirSpeed;
        }

        public override void OnFixedUpdate()
        {
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
            vy -= (data.jumpOnAirForce * Time.fixedDeltaTime);
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