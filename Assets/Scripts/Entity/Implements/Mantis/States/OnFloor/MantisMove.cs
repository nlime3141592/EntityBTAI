namespace Unchord
{
    public class MantisMove : MantisOnFloor
    {
        // fixed data
        private int m_moveFrame = 60;

        // variables
        private int m_leftMoveFrame = 0;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(false, false);

            instance.bUpdateAggroDirX = false;
            instance.bFixedLookDirByAxis.x = true;

            m_leftMoveFrame = m_moveFrame;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(m_leftMoveFrame > 0)
                --m_leftMoveFrame;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_leftMoveFrame <= 0)
                return Mantis.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.bUpdateAggroDirX = true;
            instance.bFixedLookDirByAxis.x = false;
        }
    }
}