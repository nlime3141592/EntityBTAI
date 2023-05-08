namespace Unchord
{
    public class MantisComboShout : MantisShout
    {
        public override int idConstant => Mantis.c_st_COMBO_SHOUT;

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Mantis.c_st_KNIFE_GRINDING;
            
            return MachineConstant.c_lt_PASS;
        }
    }
}