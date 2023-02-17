namespace UnchordMetroidvania
{
    public class ExcavatorOnFloor : ExcavatorState
    {
        public ExcavatorOnFloor(Excavator _instance)
        : base(_instance)
        {
            
        }

        public override int Transit()
        {
            int transit = base.Transit();
            
            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(!excavator.senseData.bOnFloor)
                return ExcavatorFsm.c_st_FREE_FALL;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}