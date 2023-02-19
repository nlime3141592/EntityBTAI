namespace UnchordMetroidvania
{
    public class ExcavatorStamping : ExcavatorAttack
    {
        public ExcavatorStamping(Excavator _instance)
        : base(_instance)
        {
            base.attackRange = new LTRB()
            {
                left = 0.0f,
                top = 0.0f,
                right = 10.5f,
                bottom = 8.0f
            };
            base.targetCount = 12;
            base.baseDamage = 1.0f;
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