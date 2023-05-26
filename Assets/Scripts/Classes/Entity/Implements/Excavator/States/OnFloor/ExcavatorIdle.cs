using UnityEngine;

namespace Unchord
{
    public class ExcavatorIdle : ExcavatorIdleBase
    {
        public override int idConstant => Excavator.c_st_IDLE;

        private float m_time_leftIdle;
        private float m_time_leftRotation;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            float weight = UnityEngine.Random.value;
            float min = instance.time_idleMin;
            float max = instance.time_idleMax;
            m_time_leftIdle = (max - min) * weight + min;

            weight = UnityEngine.Random.value;
            min = instance.time_idleRotationMin / 4;
            max = instance.time_idleRotationMax / 4;
            m_time_leftRotation = (max - min) * weight + min;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.vm.SetVelocityXY(0.0f, -1.0f);

            if(m_time_leftRotation <= 0)
            {
                instance.lookDir.x = m_GetLookDirX();

                float weight = UnityEngine.Random.value;
                float min = instance.time_idleRotationMin;
                float max = instance.time_idleRotationMax;
                m_time_leftRotation = (max - min) * weight + min;
            }
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

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(m_time_leftRotation > 0)
                m_time_leftRotation -= Time.deltaTime;

            if(m_time_leftIdle > 0)
                m_time_leftIdle -= Time.deltaTime;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_time_leftIdle <= 0)
            {
                float ox = instance.transform.position.x + instance.aiCenterOffset.x;
                float oy = instance.transform.position.y + instance.aiCenterOffset.y;
                float px = instance.aggroTargets[0].transform.position.x;
                float py = instance.aggroTargets[0].transform.position.y;
                float lx = instance.lookDir.fx;
                float ly = instance.lookDir.fy;

                if(instance.phase == 0) return instance.stateAi_001.GetState(
                    instance.prng,
                    ox, oy,
                    px, py,
                    lx, ly
                );
                else if(instance.phase == 1) return instance.stateAi_002.GetState(
                    instance.prng,
                    ox, oy,
                    px, py,
                    lx, ly
                );
                else if(instance.phase == 2) return instance.stateAi_003.GetState(
                    instance.prng,
                    ox, oy,
                    px, py,
                    lx, ly
                );
            }

            return MachineConstant.c_lt_PASS;
        }
    }
}