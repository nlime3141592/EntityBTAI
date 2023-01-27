namespace UnchordMetroidvania
{
    public abstract class PlayerAbility : PlayerState
    {
        protected bool p_bEndOfAbility;

        public PlayerAbility(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();
            p_bEndOfAbility = false;
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(p_bEndOfAbility)
            {
                if(player.senseData.bOnFloor)
                    fsm.Change(fsm.idleShort);
                else
                    fsm.Change(fsm.freeFall);
                return true;
            }

            return false;
        }
    }
}