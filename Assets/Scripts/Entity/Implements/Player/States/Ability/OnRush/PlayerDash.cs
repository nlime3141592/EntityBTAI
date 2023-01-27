namespace UnchordMetroidvania
{
    public class PlayerDash : PlayerRush
    {
        public PlayerDash(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(fsm.nextFixedFrameNumber >= data.dashFrame)
            {
                if(!p_bEndOfAbility)
                    p_bEndOfAbility = true;
                return;
            }
            else if(player.senseData.bOnWallFrontB || player.senseData.bOnWallFrontT)
            {
                if(!p_bEndOfAbility)
                    p_bEndOfAbility = true;
                return;
            }

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
            else if(player.leftAirJumpCount > 0 && player.jumpDown)
            {
                fsm.Change(fsm.jumpOnAir);
                return true;
            }

            return false;
        }
    }
}
