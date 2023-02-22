namespace UnchordMetroidvania
{
    public class ExcavatorGroggy : ExcavatorOnFloor
    {
        private PhaseController m_phaser;
        private Timer m_groggyTime;

        public ExcavatorGroggy(Excavator _instance)
        : base(_instance)
        {
            m_phaser = new PhaseController(3, 0.0f);
            m_groggyTime = new Timer(5.0f);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            excavator.aPhase = m_phaser.Next();

            if(m_phaser.current == 0)
                excavator.groggyValue = 0.0f;

            if(m_phaser.current == 1)
                m_groggyTime.Reset();
        }

        public override void OnFixedUpdate()
        {
            excavator.vm.SetVelocityY(-1.0f);
        }

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();

            if(fsm.current != ExcavatorFsm.c_st_GROGGY)
                m_phaser.Reset();
        }

        public override void OnUpdate()
        {
            if(m_phaser.current == 1 && !m_groggyTime.bEndOfTimer)
                m_groggyTime.OnUpdate();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(m_phaser.current == 0 && excavator.aController.bEndOfAnimation)
                return ExcavatorFsm.c_st_GROGGY;
            else if(m_phaser.current == 1 && m_groggyTime.bEndOfTimer)
                return ExcavatorFsm.c_st_GROGGY;
            else if(m_phaser.current == 2 && excavator.aController.bEndOfAnimation)
                return ExcavatorFsm.c_st_IDLE;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}