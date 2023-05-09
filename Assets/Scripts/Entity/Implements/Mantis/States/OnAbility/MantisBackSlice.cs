namespace Unchord
{
    public class MantisBackSlice : MantisAttack
    {
        public override int idConstant => Mantis.c_st_BACK_SLICE;

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Mantis.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnActionBegin()
        {
            if(instance.lookDir.x == Direction.Positive)
                instance.lookDir.x = Direction.Negative;
            else
                instance.lookDir.x = Direction.Positive;
        }
    }
}