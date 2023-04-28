namespace Unchord
{
    public class ExcavatorGroggy : ExcavatorOnFloor
    {
        public override int idConstant => Excavator.c_st_GROGGY;

        private PhaseController m_phaser;
        private Timer m_groggyTime;

        public override void OnConstruct()
        {
            base.OnConstruct();

            m_phaser = new PhaseController(3, 0.0f);
            m_groggyTime = new Timer(5.0f);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.aPhase = m_phaser.Next();

            if(m_phaser.current == 0)
                instance.groggyValue = 0.0f;

            if(m_phaser.current == 1)
                m_groggyTime.Reset();
        }

        public override void OnFixedUpdate()
        {
            instance.vm.SetVelocityY(-1.0f);
        }

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();

            if(instance.fsm.state != this)
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

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_phaser.current == 0 && instance.aController.bEndOfAnimation)
                return Excavator.c_st_GROGGY;
            else if(m_phaser.current == 1 && m_groggyTime.bEndOfTimer)
                return Excavator.c_st_GROGGY;
            else if(m_phaser.current == 2 && instance.aController.bEndOfAnimation)
                return Excavator.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }
    }
}