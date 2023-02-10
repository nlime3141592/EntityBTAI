using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerIdleShort : PlayerIdle
    {
        private float m_idleTime = 10.0f;
        private float m_leftIdleTime;

        public PlayerIdleShort(Player _player)
        : base(_player)
        {

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

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(m_leftIdleTime <= 0)
                return PlayerFsm.c_st_IDLE_LONG;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}