using UnityEngine;

namespace Unchord
{
    public class MantisGroggy : MantisOnFloor
    {
        private float m_groggyTime = 5.0f;
        private float m_leftGroggyTime;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.groggyValue = 0.0f; // NOTE: 테스트 코드. 점진적으로 groggyValue를 감소시키는 로직을 구현할 수도 있음.
            m_leftGroggyTime = m_groggyTime;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(m_leftGroggyTime > 0)
                m_leftGroggyTime -= Time.deltaTime;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_leftGroggyTime <= 0.0f)
                return Mantis.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }
    }
}