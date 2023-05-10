using UnityEngine;

namespace Unchord
{
    public class ExcavatorIdle : ExcavatorIdleBase
    {
        public override int idConstant => Excavator.c_st_IDLE;

        // fixed data
        private Timer m_idleTimer;
        private float m_rangeX1 = 10.5f;
        private float m_rangeX2 = 21.0f;
        private float m_rangeY1 = 4.0f;
        private float m_rangeY2 = 8.0f;

        // variable
        private int m_rangeCode = -1;

        protected override void OnConstruct()
        {
            base.OnConstruct();
            m_idleTimer = new Timer(1.2f);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            m_idleTimer.Reset();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.vm.SetVelocityXY(0.0f, -1.0f);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!m_idleTimer.bEndOfTimer)
                m_idleTimer.OnUpdate();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_idleTimer.bEndOfTimer)
            {
                float ox = instance.transform.position.x + instance.aiCenterOffset.x;
                float oy = instance.transform.position.y + instance.aiCenterOffset.y;
                float px = instance.aggroAi.targets[0].transform.position.x;
                float py = instance.aggroAi.targets[0].transform.position.y;
                float lx = instance.lookDir.fx;
                float ly = instance.lookDir.fy;

                if(instance.phase == 0) return instance.stateAi_001.GetState(
                    instance.prng,
                    ox, oy,
                    px, py,
                    lx, ly,
                    m_rangeX1, m_rangeX2,
                    m_rangeY1, m_rangeY2
                );
                else if(instance.phase == 1) return instance.stateAi_002.GetState(
                    instance.prng,
                    ox, oy,
                    px, py,
                    lx, ly,
                    m_rangeX1, m_rangeX2,
                    m_rangeY1, m_rangeY2
                );
                else if(instance.phase == 2) return instance.stateAi_003.GetState(
                    instance.prng,
                    ox, oy,
                    px, py,
                    lx, ly,
                    m_rangeX1, m_rangeX2,
                    m_rangeY1, m_rangeY2
                );
            }

            return MachineConstant.c_lt_PASS;
        }
    }
}