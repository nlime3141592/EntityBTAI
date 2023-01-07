namespace UnchordMetroidvania
{
    public class PlayerIdleWallFront : PlayerOnWallFront
    {
        public PlayerIdleWallFront(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
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

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.axisInput.x == 0)
            {
                player.fsm.Change(player.slidingWallFront);
                return true;
            }

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }
    }
}