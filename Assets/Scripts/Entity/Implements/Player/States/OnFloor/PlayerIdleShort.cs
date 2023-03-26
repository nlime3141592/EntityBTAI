using UnityEngine;

namespace Unchord
{
    public class PlayerIdleShort : PlayerIdle
    {
        private float m_idleTime = 10.0f;
        private float m_leftIdleTime;

        public override void OnMachineBegin(Player _instance, int _id)
        {
            base.OnMachineBegin(_instance, _id);

            _instance.stateMap.Add(Player.c_st_IDLE_SHORT, _id);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            m_leftIdleTime = m_idleTime;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            bool bGameStarted = GameManager.instance.bGameStarted;

            if(!bGameStarted)
                m_leftIdleTime = m_idleTime;
            else if(m_leftIdleTime > 0)
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