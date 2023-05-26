using UnityEngine;

namespace Unchord
{
    public class PlayerIdleShort : PlayerIdle
    {
        private float m_leftIdleTime;

        public override int idConstant => Player.c_st_IDLE_SHORT;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            m_leftIdleTime = instance.time_idleShort;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(m_leftIdleTime > 0)
                m_leftIdleTime -= Time.deltaTime;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_leftIdleTime <= 0)
                return Player.c_st_IDLE_LONG;

            return MachineConstant.c_lt_PASS;
        }
    }
}