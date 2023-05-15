using UnityEngine;

namespace Unchord
{
    public abstract class PlayerParrying : PlayerAbility
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.bParrying = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            instance.bParrying = instance.bBeginOfAction && !instance.bEndOfAction;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.iManager.parryingUp || instance.bEndOfAnimation)
                return Player.c_st_IDLE_SHORT;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.bParrying = false;
        }
    }
}