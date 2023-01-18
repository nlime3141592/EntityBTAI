namespace UnchordMetroidvania
{
    public abstract class _PlayerAbility : PlayerState
    {
        protected bool p_bEndOfAbility;

        public _PlayerAbility(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            p_bEndOfAbility = false;
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(p_bEndOfAbility)
            {
                if(fsm.bOnFloor)
                    fsm.Change(fsm.idleShort);
                else
                    fsm.Change(fsm.freeFall);
                return true;
            }

            return false;
        }
    }
}