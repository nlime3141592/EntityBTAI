using UnityEngine;

namespace Unchord
{
    public class MantisIdle : MantisOnFloor
    {
        public override int idConstant => Mantis.c_st_IDLE;

        // fixed data
        private float m_rangeX1 = 8.0f; // NOTE: 상태 전이 구간 설정용 변수.
        private float m_rangeX2 = 16.0f;
        private float m_rangeY1 = 4.0f;
        private float m_rangeY2 = 8.0f;

        // variable
        private int m_leftIdleTime = 0;
        private int m_rangeCode = -1;

        private int m_leftIdleAggroDelay;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePositionX();
            instance.vm.MeltPositionY();

            m_leftIdleTime = instance.prng.Next(instance.frame_idleTimeMin, instance.frame_idleTimeMax + 1);
            m_leftIdleAggroDelay = instance.prng.Next(instance.frame_idleAggroDelay / 4, instance.frame_idleAggroDelay / 2);
            // instance.lookDir.x = (Direction)(-(int)(instance.lookDir.x));
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            // TODO: 이 곳에서 idle 상태 시 어그로 영역을 이용한 타겟 감지를 수행.

            instance.vm.SetVelocityY(-1.0f);

            if(m_leftIdleAggroDelay == 0)
            {
                instance.lookDir.x = m_GetLookDirX();
                m_leftIdleAggroDelay = instance.frame_idleAggroDelay;
            }
            else
                --m_leftIdleAggroDelay;

            if(m_leftIdleTime > 0)
                --m_leftIdleTime;
        }

        private Direction m_GetLookDirX()
        {
            if(!instance.bAggro)
                return instance.lookDir.x;

            float tx = instance.aggroTargets[0].transform.position.x;
            float px = instance.transform.position.x;

            if(tx - px < 0)
                return Direction.Negative;
            else
                return Direction.Positive;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(!instance.bAggro)
                return MachineConstant.c_lt_CONTINUE;
            else if(m_leftIdleTime <= 0)
            {
                float ox = instance.aiCenter.position.x;
                float oy = instance.aiCenter.position.x;
                float px = instance.transform.position.x;
                float py = instance.transform.position.x;
                float lx = instance.lookDir.fx;
                float ly = instance.lookDir.fy;

                if(instance.phase == 1) return instance.stateAi_001.GetState(
                    instance.prng,
                    ox, oy,
                    px, py,
                    lx, ly,
                    m_rangeX1, m_rangeX2,
                    m_rangeY1, m_rangeY2
                );
                else if(instance.phase == 2) return instance.stateAi_002.GetState(
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

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.vm.MeltPositionX();
        }
    }
}