namespace UnchordMetroidvania
{
    public class PlayerDash : PlayerRush
    {
        private int m_leftDashFrame = 0;

        public PlayerDash(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();
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

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(m_leftDashFrame <= 0)
            {
                fsm.Change(fsm.freeFall);
                return true;
            }
            else if(player.leftAirJumpCount > 0 && player.jumpDown)
            {
                fsm.Change(fsm.jumpOnAir);
                return true;
            }

            return false;
        }
    }
}
