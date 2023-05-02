using UnityEngine;

namespace Unchord
{
    public class PlayerFreeFall : PlayerOnAir
    {
        public override int idConstant => Player.c_st_FREE_FALL;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = instance.bIsRun ? instance.speed_Run : instance.speed_Walk;
            float ix = instance.iManager.ix;

            instance.moveDir.x = ix;
            instance.moveDir.y = 0;
            vx *= ix;

            float vy = instance.vm.y;
            float dV = instance.gravity_FreeFall * Time.fixedDeltaTime;

            vy += dV;
            if(vy < instance.speedMin_FreeFall)
                vy = instance.speedMin_FreeFall;

            instance.vm.SetVelocityXY(vx, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;

            return MachineConstant.c_lt_PASS;
        }
    }
}