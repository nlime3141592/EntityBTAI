namespace Unchord
{
    public class MantisPhaseShout : MantisShout
    {
        public override int idConstant => Mantis.c_st_PHASE_SHOUT;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.bInvincibility = true;

            if(instance.phase == 0)
                instance.SetHealth(instance.maxHealth.finalValue);

            ++instance.phase;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Mantis.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.bInvincibility = false;
        }
    }
}