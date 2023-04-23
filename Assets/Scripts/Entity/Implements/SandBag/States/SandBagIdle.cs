using UnityEngine;

namespace Unchord
{
    public class SandBagIdle : SandBagState
    {
        public override void OnConstruct()
        {
            base.OnConstruct();

            idFixed = SandBag.c_st_IDLE;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(true, false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.vm.SetVelocityY(-10.0f);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.health <= 0)
                return SandBag.c_st_DIE;

            return MachineConstant.c_lt_PASS;
        }
    }
}