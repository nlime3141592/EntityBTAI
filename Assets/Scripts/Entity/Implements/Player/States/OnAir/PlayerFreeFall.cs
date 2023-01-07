using UnityEngine;

namespace UnchordMetroidvania
{
    public class _PlayerFreeFall : _PlayerOnAir
    {
        public _PlayerFreeFall(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = player.bIsRun ? player.runSpeed : player.walkSpeed;
            float ix = player.axisInput.x;

            player.moveDir.x = ix;
            player.moveDir.y = 0;
            vx *= ix;

            float vy = player.vm.y;
            float dV = data.gravity * Time.fixedDeltaTime;

            vy += dV;
            if(vy < data.minFreeFallSpeed)
                vy = data.minFreeFallSpeed;

            player.vm.SetVelocityXY(vx, vy);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.axisInput.y > 0)
            {
                player.fsm.Change(player.gliding);
                return true;
            }

            return false;
        }
    }
}