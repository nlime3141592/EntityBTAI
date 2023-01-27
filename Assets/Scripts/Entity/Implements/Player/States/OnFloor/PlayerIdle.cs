namespace UnchordMetroidvania
{
    public class PlayerIdle : PlayerOnFloor
    {
        public PlayerIdle(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

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
                fsm.Change(fsm.headUp);
                return true;
            }
            else if(player.axisInput.y < 0)
            {
                fsm.Change(fsm.sit);
                return true;
            }
            else if(player.axisInput.x != 0)
            {
                if(player.bIsRun)
                    fsm.Change(fsm.run);
                else
                    fsm.Change(fsm.walk);
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