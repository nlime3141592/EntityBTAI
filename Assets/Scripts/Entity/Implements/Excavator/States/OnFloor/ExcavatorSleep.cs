using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorSleep : ExcavatorState
    {
        private bool m_bInput = false;
        private bool m_bTriggered = false;

        public ExcavatorSleep(Excavator _instance)
        : base(_instance)
        {
            
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(m_bInput && !m_bTriggered)
            {
                m_bInput = false;
                m_bTriggered = true;

                float ang = 30.0f;
                float beg = -ang / 2;
                float d = beg / 2;

                for(int i = 0; i < 5; ++i)
                    TEST_ShootProjectile(Quaternion.Euler(0, 0, beg + i * d) * excavator.projVelocity);
            }
        }

        private void TEST_ShootProjectile(Vector2 velocity)
        {
            Projectile proj = excavator.projectile.Copy();
            proj.InitIgnore(excavator.terrainCollider);
            proj.InitVelocity(velocity);
            proj.InitPosition(excavator.transform.position);
            proj.InitShow();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!m_bInput)
                m_bInput = Input.GetKeyDown(KeyCode.Y);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(excavator.bAggro)
                return ExcavatorFsm.c_st_WAKE_UP;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}