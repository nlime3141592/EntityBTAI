namespace UnchordMetroidvania
{
    public class PlayerIdle : _PlayerOnFloor
    {
        public PlayerIdle(Player player, PlayerData data, int id, string name)
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
            else if(player.axisInput.y > 0)
            {
                player.fsm.Change(player.headUp);
                return true;
            }
            else if(player.axisInput.y < 0)
            {
                player.fsm.Change(player.sit);
                return true;
            }
            else if(player.axisInput.x != 0)
            {
                if(player.bIsRun)
                    player.fsm.Change(player.run);
                else
                    player.fsm.Change(player.walk);
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