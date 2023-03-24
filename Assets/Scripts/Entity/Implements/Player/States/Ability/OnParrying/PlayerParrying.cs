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

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            // instance.senseData.UpdateMoveDir(player);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            instance.bParrying = instance.aController.bBeginOfAction && !instance.aController.bEndOfAction;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.parryingUp || instance.aController.bEndOfAnimation)
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