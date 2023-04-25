namespace Unchord
{
    public class MantisWalkBack : MantisWalk
    {
        public override int idConstant => Mantis.c_st_WALK_BACK;

        private float m_ix;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            m_ix = -instance.lookDir.fx;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = m_ix * instance.moveDir.x * instance.walkSpeed;
            float vy = m_ix * instance.moveDir.y * instance.walkSpeed;

            instance.vm.SetVelocityXY(vx, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.senseData.bOnWallBack)
                return Mantis.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }
    }
}