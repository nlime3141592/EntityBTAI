using UnityEngine;

namespace Unchord
{
    public class ExcavatorProjectileExplosion : ExcavatorProjectileState // , IBattleState
    {
        public override int idConstant => ExcavatorProjectile.c_st_EXPLOSION;

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
            else if(instance.aController.bEndOfAnimation)
                return MachineConstant.c_st_MACHINE_OFF;
            
            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.SetHealth(0);
        }
    }
}