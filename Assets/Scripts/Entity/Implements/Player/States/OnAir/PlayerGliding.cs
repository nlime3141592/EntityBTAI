using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerGliding : PlayerOnAir
    {
        public PlayerGliding(Player _player)
        : base(_player)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = player.bIsRun ? data.runSpeed : data.walkSpeed;
            float ix = player.axisInput.x;

            player.moveDir.x = ix;
            player.moveDir.y = 0;
            vx *= ix;

            float vy = player.vm.y;
            float dV = data.glidingAirForce * Time.fixedDeltaTime;
            float diffV = vy - data.glidingSpeed;
            float dirY = 0;

            if(diffV > 0)
                dirY = -1;
            else if(diffV < 0)
                dirY = 1;

            vy += dV * dirY;

            if(dirY < 0 && vy < data.glidingSpeed)
                vy = data.glidingSpeed;
            else if(dirY > 0 && vy > data.glidingSpeed)
                vy = data.glidingSpeed;

            player.vm.SetVelocityXY(vx, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.axisInput.y == 0)
                return PlayerFsm.c_st_FREE_FALL;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}