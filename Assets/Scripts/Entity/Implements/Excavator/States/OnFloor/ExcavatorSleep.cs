using UnityEngine;

namespace Unchord
{
    public class ExcavatorSleep : ExcavatorState
    {
        public override int idConstant => Excavator.c_st_SLEEP;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aggroAi.bAggro)
                return Excavator.c_st_WAKE_UP;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnAggroBegin()
        {
            base.OnAggroBegin();

            instance.aggroAi.sensor.box.l = 200;
            instance.aggroAi.sensor.box.t = 200;
            instance.aggroAi.sensor.box.r = 200;
            instance.aggroAi.sensor.box.b = 200;
        }
    }
}