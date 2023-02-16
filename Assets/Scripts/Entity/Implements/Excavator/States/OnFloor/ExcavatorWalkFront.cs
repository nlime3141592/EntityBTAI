namespace UnchordMetroidvania
{
    public class ExcavatorWalkFront : ExcavatorMove
    {
        private float m_ix;

        public ExcavatorWalkFront(Excavator _instance)
        : base(_instance)
        {
            
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            m_ix = excavator.lookDir.x;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = m_ix * excavator.moveDir.x * data.walkSpeed;
            float vy = m_ix * excavator.moveDir.y * data.walkSpeed - 0.1f;

            excavator.vm.SetVelocityXY(vx, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(excavator.aController.bEndOfAnimation)
                return ExcavatorFsm.c_st_IDLE;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}