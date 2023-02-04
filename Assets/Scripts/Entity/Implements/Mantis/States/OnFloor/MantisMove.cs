namespace UnchordMetroidvania
{
    public class MantisMove : MantisOnFloor
    {
        // fixed data
        private int m_moveFrame = 60;

        // variables
        private int m_leftMoveFrame = 0;

        public MantisMove(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

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

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(m_leftMoveFrame <= 0)
            {
                fsm.Change(fsm.idle);
                return true;
            }

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            mantis.bUpdateAggroDirX = true;
            mantis.bFixLookDirX = false;
        }
    }
}