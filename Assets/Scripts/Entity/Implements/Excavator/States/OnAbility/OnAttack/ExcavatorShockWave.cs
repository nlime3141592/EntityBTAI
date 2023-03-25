using UnityEngine;

namespace Unchord
{
    public class ExcavatorShockWave : ExcavatorAttack
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.bUpdateAggroDirX = false;
            instance.bFixLookDir.x = true;
            instance.aController.onBeginOfAction += m_OnActionBegin;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Excavator.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.aController.onBeginOfAction -= m_OnActionBegin;
        }

        private void m_OnActionBegin()
        {
            ShockWave lw = instance.shockwave.Copy();
            ShockWave rw = instance.shockwave.Copy();
            float dx = (instance.shockRange.left + instance.shockRange.right) * 0.5f;

            lw.InitIgnore(instance.hitColliders);
            lw.InitBaseDamage(1.0f);
            lw.InitDirection(-1);
            lw.InitPosition(instance.aiCenter.position - new Vector3(dx, 0, 0));
            lw.InitRange(instance.shockRange);
            lw.InitLeftWave(15);
            lw.InitShow();

            rw.InitIgnore(instance.hitColliders);
            rw.InitBaseDamage(1.0f);
            rw.InitDirection(1);
            rw.InitPosition(instance.aiCenter.position + new Vector3(dx, 0, 0));
            rw.InitRange(instance.shockRange);
            rw.InitLeftWave(15);
            rw.InitShow();
        }
    }
}