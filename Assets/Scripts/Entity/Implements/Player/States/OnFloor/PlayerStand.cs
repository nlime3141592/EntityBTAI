namespace UnchordMetroidvania
{
    public abstract class PlayerStand : _PlayerOnFloor
    {
        public PlayerStand(Player player, PlayerData data, int id, string name)
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
            player.vm.SetVelocityY(-1.0f);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.axisInput.y == 0)
            {
                player.fsm.Change(player.idleShort);
                return true;
            }

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            player.vm.MeltPositionX();
        }
    }
}