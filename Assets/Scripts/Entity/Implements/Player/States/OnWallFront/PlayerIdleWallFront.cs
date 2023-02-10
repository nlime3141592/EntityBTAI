namespace UnchordMetroidvania
{
    public class PlayerIdleWallFront : PlayerOnWallFront
    {
        public PlayerIdleWallFront(Player _player)
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
            player.vm.SetVelocityXY(0.0f, 0.0f);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.axisInput.x == 0)
                return PlayerFsm.c_st_SLIDING_WALL_FRONT;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }
    }
}