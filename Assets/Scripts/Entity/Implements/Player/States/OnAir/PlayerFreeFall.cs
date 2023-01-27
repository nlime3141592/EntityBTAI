using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerFreeFall : PlayerOnAir
    {
        public PlayerFreeFall(Player _player, int _id, string _name)
        : base(_player, _id, _name)
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
                fsm.Change(fsm.gliding);
                return true;
            }

            return false;
        }
    }
}