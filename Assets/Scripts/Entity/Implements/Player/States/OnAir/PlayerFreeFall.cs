using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerFreeFall : PlayerOnAir
    {
        public PlayerFreeFall(Player _player)
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
            float dV = data.gravity * Time.fixedDeltaTime;

            vy += dV;
            if(vy < data.minFreeFallSpeed)
                vy = data.minFreeFallSpeed;

            player.vm.SetVelocityXY(vx, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.axisInput.y > 0)
                return PlayerFsm.c_st_GLIDING;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}