namespace UnchordMetroidvania
{
    public abstract class PlayerAbility : PlayerState
    {
        protected bool p_bEndOfAbility;

        public PlayerAbility(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            p_bEndOfAbility = false;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(p_bEndOfAbility)
            {
                if(player.senseData.bOnFloor)
                    return PlayerFsm.c_st_IDLE_SHORT;
                else
                    return PlayerFsm.c_st_FREE_FALL;
            }

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}