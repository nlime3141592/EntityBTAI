namespace Unchord
{
    public class PlayerDash : PlayerRush
    {
        private int m_leftDashFrame = 0;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            --instance.countLeft_Dash;
            m_leftDashFrame = instance.frame_Dash;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(instance.senseData.bOnWallFrontB || instance.senseData.bOnWallFrontT)
            {
                m_leftDashFrame = 0;
                return;
            }

            if(m_leftDashFrame > 0)
                --m_leftDashFrame;

            instance.moveDir.x = 1;
            instance.moveDir.y = 0;

            float vx = instance.lookDir.fx * instance.moveDir.x * instance.speed_Dash;
            float vy = 0;

            instance.vm.SetVelocityXY(vx, vy);
        }

        public override bool CanTransit()
        {
            return base.CanTransit() && instance.countLeft_Dash > 0;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_leftDashFrame <= 0)
                return Player.c_st_FREE_FALL;
            else if(instance.countLeft_JumpOnAir > 0 && instance.jumpDown)
                return Player.c_st_JUMP_ON_AIR;

            return MachineConstant.c_lt_PASS;
        }
    }
}
