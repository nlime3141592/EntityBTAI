using UnityEngine;

namespace UnchordMetroidvania
{
    public class _PlayerJumpOnAir : _PlayerJump
    {
        private float vy;

        public _PlayerJumpOnAir(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            --(fsm.leftAirJumpCount);
            vy = data.jumpOnAirSpeed;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = fsm.bIsRun ? data.runSpeed : data.walkSpeed;
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