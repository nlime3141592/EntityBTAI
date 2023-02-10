namespace UnchordMetroidvania
{
    public class MantisShout : MantisOnFloor
    {
        public MantisShout(Mantis _mantis)
        : base(_mantis)
        {
            
        }

        public override int Transit()
        {
            if(!mantis.aController.bEndOfAnimation)
                return FiniteStateMachine.c_st_STATE_CONTINUE;
            else if(fsm.mode == 1 && mantis.health <= 0)
                return MantisFsm.c_st_KNIFE_GRINDING;

            int transit = base.Transit();

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}