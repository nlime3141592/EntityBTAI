namespace UnchordMetroidvania
{
    public class ExcavatorIdle : ExcavatorIdleBase
    {
        private int m_frame;
        private int m_leftFrame;

        public ExcavatorIdle(Excavator _instance)
        : base(_instance)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            excavator.vm.SetVelocityXY(0.0f, -1.0f);

            if(m_leftFrame > 0)
                --m_leftFrame;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(excavator.axisInput.x != 0)
                return ExcavatorFsm.c_st_WALK;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}