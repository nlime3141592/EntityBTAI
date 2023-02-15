namespace UnchordMetroidvania
{
    public class ExcavatorMove : ExcavatorOnFloor
    {
        public ExcavatorMove(Excavator _instance)
        : base(_instance)
        {
            
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            
            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}