namespace Unchord
{
    public class MantisDie : MantisOnFloor
    {
        public override int idConstant => Mantis.c_st_DIE;

        public override int Transit()
        {
            if(instance.aController.bEndOfAnimation)
                return MachineConstant.c_st_MACHINE_OFF;

            return MachineConstant.c_lt_PASS;
        }
    }
}