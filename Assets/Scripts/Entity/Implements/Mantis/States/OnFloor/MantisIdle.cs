namespace UnchordMetroidvania
{
    public class _MantisIdle : MantisOnFloor
    {
        // fixed data
        private int m_minIdleTime = 200;
        private int m_maxIdleTime = 400;
        private int m_ixDelayTime = 100; // 좌, 우 반전을 너무 자주 하면 안 됨.

        // variable
        private int m_leftIdleTime = 0;
        private int m_leftIxDelayTime = 0;

        public _MantisIdle(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            mantis.vm.FreezePositionX();
            mantis.vm.MeltPositionY();

            m_leftIdleTime = mantis.prng.Next(m_minIdleTime, m_maxIdleTime + 1);
            m_leftIxDelayTime = 0;
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
                // NOTE: 테스트 로직
                int weight = mantis.prng.Next(100);
                int lw = mantis.prng.Next(2);
                if(weight < 30 && !mantis.senseData.bOnWallBack || mantis.senseData.bOnWallFront)
                    fsm.Change(fsm.walkBack);
                else
                    fsm.Change(fsm.walkFront);
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