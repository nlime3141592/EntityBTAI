using UnityEngine;

namespace UnchordMetroidvania
{
    public class MantisIdle : MantisOnFloor
    {
        // fixed data
        private int m_minIdleTime = 60;
        private int m_maxIdleTime = 120;
        private int m_ixDelayTime = 30; // 좌, 우 반전을 너무 자주 하면 안 됨.

        // variable
        private int m_leftIdleTime = 0;
        private int m_leftIxDelayTime = 0;

        public MantisIdle(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            mantis.vm.FreezePositionX();
            mantis.vm.MeltPositionY();

            m_leftIdleTime = mantis.prng.Next(m_minIdleTime, m_maxIdleTime + 1);
            m_leftIxDelayTime = m_ixDelayTime / 2;
            mantis.lookDir.x = -mantis.lookDir.x;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            mantis.vm.SetVelocityY(-1.0f);

            if(m_leftIxDelayTime <= 0)
            {
                mantis.axisInput.x = mantis.GetAxisInputX();
                m_leftIxDelayTime = m_ixDelayTime;
            }

            if(m_leftIdleTime > 0)
                --m_leftIdleTime;
            if(m_leftIxDelayTime > 0)
                --m_leftIxDelayTime;
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            if(!mantis.bAggro)
                return true;
            if(m_leftIdleTime <= 0)
            {
                if(mantis.senseData.bOnWallFront)
                {
                    fsm.Change(fsm.walkBack);
                    return true;
                }
                else if(mantis.senseData.bOnWallBack)
                {
                    fsm.Change(fsm.walkFront);
                    return true;
                }
                else if(mantis.prng.Next(0, 100) < 50)
                {
                    fsm.Change(fsm.walkFront);
                    return true;
                }
                else
                {
                    fsm.Change(fsm.walkBack);
                    return true;
                }
            }

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            mantis.vm.MeltPositionX();
        }
    }
}