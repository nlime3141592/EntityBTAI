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
            p_bEndOfAbility = false;
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(p_bEndOfAbility)
            {
                if(player.bOnFloor)
                    player.fsm.Change(player.idleShort);
                else
                    player.fsm.Change(player.freeFall);
                return true;
            }

            return false;
        }
    }
}