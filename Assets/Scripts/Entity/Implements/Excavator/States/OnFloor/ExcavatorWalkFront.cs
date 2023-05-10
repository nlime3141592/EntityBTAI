namespace Unchord
{
    public class ExcavatorWalkFront : ExcavatorWalk
    {
        public override int idConstant => Excavator.c_st_WALK;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = instance.lookDir.fx * instance.moveDir.x * instance.walkSpeed;
            float vy = -0.1f;

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