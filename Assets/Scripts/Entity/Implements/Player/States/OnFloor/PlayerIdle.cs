namespace UnchordMetroidvania
{
    public class PlayerIdle : PlayerOnFloor
    {
        public PlayerIdle(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.vm.FreezePositionX();
            player.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.vm.SetVelocityY(-1.0f);
        }

        public override int Transit()
        {
            int transit = base.Transit();
            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.jumpDown)
                return PlayerFsm.c_st_JUMP_ON_FLOOR;
            else if(player.axisInput.y > 0)
                return PlayerFsm.c_st_HEAD_UP;
            else if(player.axisInput.y < 0)
                return PlayerFsm.c_st_SIT;
            else if(player.axisInput.x != 0)
            {
                if(player.bIsRun)
                    return PlayerFsm.c_st_RUN;
                else
                    return PlayerFsm.c_st_WALK;
            }

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            player.vm.MeltPositionX();
        }
    }
}