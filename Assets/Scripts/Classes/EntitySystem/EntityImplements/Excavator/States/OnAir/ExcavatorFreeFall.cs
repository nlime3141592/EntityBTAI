using UnityEngine;

namespace Unchord
{
    public class ExcavatorFreeFall : ExcavatorOnAir
    {
        public override int idConstant => Excavator.c_st_FREE_FALL;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vy = instance.vm.y;
            float dV = instance.gravity * Time.fixedDeltaTime;

            vy += dV;
            if(vy < instance.speed_freeFallMin)
                vy = instance.speed_freeFallMin;

            instance.vm.SetVelocityXY(0.0f, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.senseData.datFloorBack.bOnHit || instance.senseData.datFloorFront.bOnHit)
                return Excavator.c_st_BASIC_LANDING;

            return MachineConstant.c_lt_PASS;
        }
    }
}