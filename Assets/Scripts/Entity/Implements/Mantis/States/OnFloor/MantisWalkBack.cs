namespace UnchordMetroidvania
{
    public class MantisWalkBack : MantisWalk
    {
        private float m_ix;

        public MantisWalkBack(Mantis _mantis)
        : base(_mantis)
        {
            
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            m_ix = -mantis.lookDir.x;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = m_ix * mantis.moveDir.x * data.walkSpeed;
            float vy = m_ix * mantis.moveDir.y * data.walkSpeed;

            mantis.vm.SetVelocityXY(vx, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(mantis.senseData.bOnWallBack)
                return MantisFsm.c_st_IDLE;
            
            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}