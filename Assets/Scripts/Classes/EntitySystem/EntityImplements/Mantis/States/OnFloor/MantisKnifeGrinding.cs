namespace Unchord
{
    public class MantisKnifeGrinding : MantisOnFloor
    {
        public override int idConstant => Mantis.c_st_KNIFE_GRINDING;

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            // else if(instance.aController.bEndOfAnimation)
                return Mantis.c_st_IDLE;

            // return MachineConstant.c_lt_PASS;
        }
    }
}