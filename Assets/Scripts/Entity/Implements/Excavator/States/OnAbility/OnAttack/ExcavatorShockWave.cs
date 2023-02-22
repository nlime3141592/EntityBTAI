namespace UnchordMetroidvania
{
    public class ExcavatorShockWave : ExcavatorAttack
    {
        public ExcavatorShockWave(Excavator _instance)
        : base(_instance)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            excavator.bUpdateAggroDirX = false;
            excavator.bFixLookDirX = true;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(excavator.aController.bEndOfAnimation)
                return ExcavatorFsm.c_st_IDLE;
            
            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}