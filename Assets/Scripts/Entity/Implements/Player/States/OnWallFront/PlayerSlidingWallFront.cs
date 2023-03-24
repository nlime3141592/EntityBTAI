using UnityEngine;

namespace Unchord
{
    public class PlayerSlidingWallFront : PlayerOnWallFront
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePositionX();
            instance.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vy = instance.vm.y;
            float dV = instance.gravity_WallSlidingFront * Time.fixedDeltaTime;

            vy += dV;
            if(vy < instance.speedMin_WallSlidingFront)
                vy = instance.speedMin_WallSlidingFront;

            instance.vm.SetVelocityXY(0.0f, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.axis.x != 0)
                return Player.c_st_IDLE_WALL_FRONT;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.vm.MeltPositionX();
            instance.vm.MeltPositionY();
        }
    }
}