namespace Unchord
{
    public abstract class PlayerIdle : PlayerOnFloor
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
            instance.vm.SetVelocityY(-10.0f);
        }

        public override int Transit()
        {
            int transit = base.Transit();
            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.iManager.jumpDown)
                return Player.c_st_JUMP_ON_FLOOR;
            else if(instance.iManager.iy > 0)
                return Player.c_st_HEAD_UP;
            else if(instance.iManager.iy < 0)
                return Player.c_st_SIT;
            else if(instance.iManager.ix != 0)
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