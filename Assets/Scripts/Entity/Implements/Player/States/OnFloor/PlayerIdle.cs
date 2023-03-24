namespace Unchord
{
    public class PlayerIdle : PlayerOnFloor
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePositionX();
            instance.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.vm.SetVelocityY(-1.0f);
        }

        public override int Transit()
        {
            int transit = base.Transit();
            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.jumpDown)
                return Player.c_st_JUMP_ON_FLOOR;
            else if(instance.axis.y > 0)
                return Player.c_st_HEAD_UP;
            else if(instance.axis.y < 0)
                return Player.c_st_SIT;
            else if(instance.axis.x != 0)
            {
                if(instance.bIsRun)
                    return Player.c_st_RUN;
                else
                    return Player.c_st_WALK;
            }

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.vm.MeltPositionX();
        }
    }
}