namespace Unchord
{
    public class ExcavatorWalkFront : ExcavatorWalk
    {
        public override int idConstant => Excavator.c_st_WALK;

        private float m_ix;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            m_ix = instance.lookDir.fx;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = m_ix * instance.moveDir.x * instance.walkSpeed;
            float vy = m_ix * instance.moveDir.y * instance.walkSpeed - 0.1f;

            instance.vm.SetVelocityXY(vx, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Excavator.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }
    }
}