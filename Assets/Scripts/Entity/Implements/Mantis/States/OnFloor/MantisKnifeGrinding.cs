namespace UnchordMetroidvania
{
    public class MantisKnifeGrinding : MantisOnFloor
    {
        public MantisKnifeGrinding(Mantis _mantis)
        : base(_mantis)
        {
            
        }

        public override int Transit()
        {
            if(!mantis.aController.bEndOfAnimation)
                return FiniteStateMachine.c_st_STATE_CONTINUE;
            else if(fsm.mode == 1 && mantis.health <= 0)
                return MantisFsm.c_st_IDLE;

            int transit = base.Transit();

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            if(fsm.mode == 1 && mantis.health <= 0)
            {
                fsm.mode = 2; // 2페이즈로 진입합니다.
                mantis.health = mantis.maxHealth.finalValue;
            }
        }
    }
}