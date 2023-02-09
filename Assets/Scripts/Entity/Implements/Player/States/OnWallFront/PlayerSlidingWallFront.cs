using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerSlidingWallFront : PlayerOnWallFront
    {
        public PlayerSlidingWallFront(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.vm.FreezePositionX();
            player.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vy = player.vm.y;
            float dV = data.slidingWallGravity * Time.fixedDeltaTime;

            vy += dV;
            if(vy < data.minSlidingWallFrontSpeed)
                vy = data.minSlidingWallFrontSpeed;

            player.vm.SetVelocityXY(0.0f, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.axisInput.x != 0)
                return PlayerFsm.c_st_IDLE_WALL_FRONT;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }
    }
}