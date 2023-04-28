using UnityEngine;

namespace Unchord
{
    public class ExcavatorSleep : ExcavatorState
    {
        public override int idConstant => Excavator.c_st_SLEEP;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.monsterPhase = 1;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
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
    }
}