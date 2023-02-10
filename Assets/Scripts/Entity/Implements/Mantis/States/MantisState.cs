namespace UnchordMetroidvania
{
    public abstract class MantisState : MonsterState<Mantis>
    {
        protected MantisFsm fsm => instance.fsm;
        protected MantisData data => instance.data;
        protected Mantis mantis => instance;
        // protected MantisFsm fsm => instance.fsm;

        public MantisState(Mantis _mantis)
        : base(_mantis)
        {
            
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            mantis.senseData.UpdateData(mantis);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(mantis.health <= 0)
            {
                if(fsm.mode == 1)
                    return MantisFsm.c_st_SHOUT;
                else if(fsm.mode == 2)
                    return MantisFsm.c_st_DIE;
            }
            else if(mantis.groggyValue >= 1.0f)
                return MantisFsm.c_st_GROGGY;
            else if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}