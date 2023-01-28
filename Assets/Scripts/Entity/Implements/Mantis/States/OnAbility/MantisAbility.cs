namespace UnchordMetroidvania
{
    public abstract class MantisAbility : MantisState
    {
        protected bool p_bEndOfAbility;

        public MantisAbility(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
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
                /*
                if(player.senseData.bOnFloor)
                    fsm.Change(fsm.idle);
                else
                    fsm.Change(fsm.freeFall);
                */
                return true;
            }

            return false;
        }
    }
}