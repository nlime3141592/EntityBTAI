using UnityEngine;

namespace UnchordMetroidvania
{
    public class _PlayerGliding : _PlayerOnAir
    {
        public _PlayerGliding(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
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

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.axisInput.y == 0)
            {
                player.fsm.Change(player.freeFall);
                return true;
            }

            return false;
        }
    }
}