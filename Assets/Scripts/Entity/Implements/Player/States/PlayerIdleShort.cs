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

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(fsm.fps >= data.shortIdleFrame)
                fsm.Change(player.idleLong);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
        }
    }
}