namespace UnchordMetroidvania
{
    public class PlayerIdleShort : PlayerIdle
    {
        public PlayerIdleShort(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(fsm.nextFixedFrameNumber >= data.shortIdleFrame)
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