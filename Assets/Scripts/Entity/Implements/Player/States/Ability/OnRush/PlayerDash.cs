namespace UnchordMetroidvania
{
    public class PlayerDash : PlayerRush
    {
        private int m_leftDashFrame = 0;

        public PlayerDash(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            --player.leftDashCount;
            m_leftDashFrame = data.dashFrame;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(player.senseData.bOnWallFrontB || player.senseData.bOnWallFrontT)
            {
                m_leftDashFrame = 0;
                return;
            }

            if(m_leftDashFrame > 0)
                --m_leftDashFrame;

            player.moveDir.x = 1;
            player.moveDir.y = 0;

            float vx = player.lookDir.x * player.moveDir.x * data.dashSpeed;
            float vy = 0;

            player.vm.SetVelocityXY(vx, vy);
        }

        public override bool CanTransit()
        {
            return base.CanTransit() && player.leftDashCount > 0;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(m_leftDashFrame <= 0)
                return PlayerFsm.c_st_FREE_FALL;
            else if(player.leftAirJumpCount > 0 && player.jumpDown)
                return PlayerFsm.c_st_JUMP_ON_AIR;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}
