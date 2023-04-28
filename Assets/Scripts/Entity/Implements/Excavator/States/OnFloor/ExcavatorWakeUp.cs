namespace Unchord
{
    public class ExcavatorWakeUp : ExcavatorIdleBase
    {
        public override int idConstant => Excavator.c_st_WAKE_UP;

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Excavator.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }
    }
}