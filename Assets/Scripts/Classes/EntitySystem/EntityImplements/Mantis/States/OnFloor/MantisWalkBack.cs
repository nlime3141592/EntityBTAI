namespace Unchord
{
    public class MantisWalkBack : MantisWalk
    {
        public override int idConstant => Mantis.c_st_WALK_BACK;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = -instance.lookDir.fx * instance.moveDir.x * instance.walkSpeed;
            float vy = -0.1f;

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