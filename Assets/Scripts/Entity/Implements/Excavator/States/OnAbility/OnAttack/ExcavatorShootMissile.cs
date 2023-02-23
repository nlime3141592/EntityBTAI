using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorShootMissile : ExcavatorAttack
    {
        private int i = -1;
        float ang = 30.0f;

        public ExcavatorShootMissile(Excavator _instance)
        : base(_instance)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            excavator.bUpdateAggroDirX = false;
            excavator.bFixLookDirX = true;

            excavator.aController.onBeginOfAction += m_OnBeginOfAction;
            i = -1;
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
            excavator.aController.onBeginOfAction -= m_OnBeginOfAction;
        }

        private void m_OnBeginOfAction()
        {
            base.OnActionBegin();
            excavator.aController.bBeginOfAction = false;

            float beg = -ang / 2;
            float d = beg / 2;
            float lx = excavator.lookDir.x;
            Vector2 finalVelocity = excavator.projVelocity;
            finalVelocity.x *= lx;
            m_ShootProjectile(Quaternion.Euler(0, 0, beg + ++i * d) * finalVelocity);
        }

        private void m_ShootProjectile(Vector2 velocity)
        {
            Projectile proj = excavator.projectile.Copy();
            proj.InitIgnore(excavator.hitColliders);
            proj.InitVelocity(velocity);
            proj.InitPosition(excavator.transform.position);
            proj.InitShow();
        }
    }
}