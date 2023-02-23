using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorShockWave : ExcavatorAttack
    {
        public ExcavatorShockWave(Excavator _instance)
        : base(_instance)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            excavator.bUpdateAggroDirX = false;
            excavator.bFixLookDirX = true;
            excavator.aController.onBeginOfAction += m_OnActionBegin;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(excavator.aController.bEndOfAnimation)
                return ExcavatorFsm.c_st_IDLE;
            
            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            excavator.aController.onBeginOfAction -= m_OnActionBegin;
        }

        private void m_OnActionBegin()
        {
            ShockWave lw = excavator.shockwave.Copy();
            ShockWave rw = excavator.shockwave.Copy();
            float dx = (excavator.shockRange.left + excavator.shockRange.right) * 0.5f;

            lw.InitIgnore(excavator.hitColliders);
            lw.InitBaseDamage(1.0f);
            lw.InitDirection(-1);
            lw.InitPosition(excavator.aiCenter.position - new Vector3(dx, 0, 0));
            lw.InitRange(excavator.shockRange);
            lw.InitLeftWave(15);
            lw.InitShow();

            rw.InitIgnore(excavator.hitColliders);
            rw.InitBaseDamage(1.0f);
            rw.InitDirection(1);
            rw.InitPosition(excavator.aiCenter.position + new Vector3(dx, 0, 0));
            rw.InitRange(excavator.shockRange);
            rw.InitLeftWave(15);
            rw.InitShow();
        }
    }
}