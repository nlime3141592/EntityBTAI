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
                excavator.projectile.Ignore(excavator.terrainCollider, excavator.projVelocity);
            }
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