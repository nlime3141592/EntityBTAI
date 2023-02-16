namespace UnchordMetroidvania
{
    public class ExcavatorBreakFloor : ExcavatorAbility
    {
        public ExcavatorBreakFloor(Excavator _instance)
        : base(_instance)
        {

        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(!excavator.senseData.bOnFloor)
                return ExcavatorFsm.c_st_IDLE;
            
            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}