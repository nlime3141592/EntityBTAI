namespace Unchord
{
    public class MantisShout : MantisOnFloor
    {
        public override int idConstant => Mantis.c_st_SHOUT;

        public override int Transit()
        {
            if(!instance.aController.bEndOfAnimation)
                return MachineConstant.c_lt_CONTINUE;
            else if(instance.phase == 1 && instance.health <= 0)
                return Mantis.c_st_KNIFE_GRINDING;

            int transit = base.Transit();

            return MachineConstant.c_lt_PASS;
        }
    }
}