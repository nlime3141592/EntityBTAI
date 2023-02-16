namespace UnchordMetroidvania
{
    public class ExcavatorWakeUp : ExcavatorIdleBase
    {
        public ExcavatorWakeUp(Excavator _instance)
        : base(_instance)
        {
            
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(excavator.bAggro)
                return ExcavatorFsm.c_st_IDLE;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}