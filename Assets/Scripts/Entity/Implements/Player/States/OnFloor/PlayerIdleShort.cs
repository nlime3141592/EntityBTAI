namespace UnchordMetroidvania
{
    public class PlayerIdleShort : PlayerIdle
    {
        public PlayerIdleShort(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(fsm.nextFps >= data.shortIdleFrame)
            {
                fsm.Change(fsm.idleLong);
                return true;
            }

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
        }
    }
}