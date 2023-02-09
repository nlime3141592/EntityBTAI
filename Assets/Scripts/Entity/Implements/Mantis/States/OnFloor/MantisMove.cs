namespace UnchordMetroidvania
{
    public class MantisMove : MantisOnFloor
    {
        // fixed data
        private int m_moveFrame = 60;

        // variables
        private int m_leftMoveFrame = 0;

        public MantisMove(Mantis _mantis)
        : base(_mantis)
        {
            
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            mantis.vm.FreezePosition(false, false);

            mantis.bUpdateAggroDirX = false;
            mantis.bFixLookDirX = true;

            m_leftMoveFrame = m_moveFrame;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            mantis.senseData.UpdateMoveDir(mantis);

            if(m_leftMoveFrame > 0)
                --m_leftMoveFrame;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(m_leftMoveFrame <= 0)
                return MantisFsm.c_st_IDLE;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            mantis.bUpdateAggroDirX = true;
            mantis.bFixLookDirX = false;
        }
    }
}