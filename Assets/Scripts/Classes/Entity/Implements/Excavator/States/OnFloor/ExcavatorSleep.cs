using UnityEngine;

namespace Unchord
{
    public class ExcavatorSleep : ExcavatorState
    {
        public override int idConstant => Excavator.c_st_SLEEP;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.lookDir.x = Direction.Negative;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bAggro)
                return Excavator.c_st_WAKE_UP;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnAggroBegin(SET_EntityAggression _aggModule)
        {
            base.OnAggroBegin(_aggModule);

            _aggModule.boxSensor.box.l = 200;
            _aggModule.boxSensor.box.t = 200;
            _aggModule.boxSensor.box.r = 200;
            _aggModule.boxSensor.box.b = 200;
        }
    }
}