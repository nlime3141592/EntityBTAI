namespace Unchord
{
    public class PlayerIdleWallFront : PlayerOnWallFront
    {
        public override int idConstant => Player.c_st_IDLE_WALL_FRONT;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePositionX();
            instance.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.vm.SetVelocityXY(0.0f, 0.0f);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.axis.x == 0)
                return Player.c_st_SLIDING_WALL_FRONT;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.vm.MeltPositionX();
            instance.vm.MeltPositionY();
        }
    }
}