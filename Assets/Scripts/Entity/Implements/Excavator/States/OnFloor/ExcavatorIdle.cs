using UnityEngine;

namespace Unchord
{
    public class ExcavatorIdle : ExcavatorIdleBase
    {
        public override int idConstant => Excavator.c_st_IDLE;

        private float m_time_leftIdle;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            float weight = UnityEngine.Random.value;
            float min = instance.time_idleMin;
            float max = instance.time_idleMax;
            m_time_leftIdle = (max - min) * weight + min;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.vm.SetVelocityXY(0.0f, -1.0f);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

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
                float px = instance.aggroAi.targets[0].transform.position.x;
                float py = instance.aggroAi.targets[0].transform.position.y;
                float lx = instance.lookDir.fx;
                float ly = instance.lookDir.fy;
                float rx1 = instance.rangeAi3_rx1;
                float rx2 = instance.rangeAi3_rx2;
                float ry1 = instance.rangeAi3_ry1;
                float ry2 = instance.rangeAi3_ry2;

                if(instance.phase == 0) return instance.stateAi_001.GetState(
                    instance.prng,
                    ox, oy,
                    px, py,
                    lx, ly,
                    rx1, rx2,
                    ry1, ry2
                );
                else if(instance.phase == 1) return instance.stateAi_002.GetState(
                    instance.prng,
                    ox, oy,
                    px, py,
                    lx, ly,
                    rx1, rx2,
                    ry1, ry2
                );
                else if(instance.phase == 2) return instance.stateAi_003.GetState(
                    instance.prng,
                    ox, oy,
                    px, py,
                    lx, ly,
                    rx1, rx2,
                    ry1, ry2
                );
            }

            return MachineConstant.c_lt_PASS;
        }
    }
}