using UnityEngine;

namespace Unchord
{
    public class ExcavatorProjectileIdle : ExcavatorProjectileState
    {
        public override int idConstant => ExcavatorProjectile.c_st_IDLE;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(true, true);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bInstanceReady)
                return ExcavatorProjectile.c_st_FLYING;
            
            return MachineConstant.c_lt_PASS;
        }
    }
}