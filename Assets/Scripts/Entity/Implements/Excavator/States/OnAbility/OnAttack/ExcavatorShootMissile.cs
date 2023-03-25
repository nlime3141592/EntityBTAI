using UnityEngine;

namespace Unchord
{
    public class ExcavatorShootMissile : ExcavatorAttack
    {
        private int i = -1;
        float ang = 30.0f;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.bUpdateAggroDirX = false;
            instance.bFixLookDir.x = true;

            instance.aController.onBeginOfAction += m_OnBeginOfAction;
            i = -1;
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
            instance.aController.onBeginOfAction -= m_OnBeginOfAction;
        }

        private void m_OnBeginOfAction()
        {
            base.OnActionBegin();
            instance.aController.bBeginOfAction = false;

            float beg = -ang / 2;
            float d = beg / 2;
            float lx = instance.lookDir.fx;
            Vector2 finalVelocity = instance.projVelocity;
            finalVelocity.x *= lx;
            m_ShootProjectile(Quaternion.Euler(0, 0, beg + ++i * d) * finalVelocity);
        }

        private void m_ShootProjectile(Vector2 velocity)
        {
            Projectile proj = instance.projectile.Copy();
            proj.InitIgnore(instance.hitColliders);
            proj.InitVelocity(velocity);
            proj.InitPosition(instance.transform.position);
            proj.InitShow();
        }
    }
}