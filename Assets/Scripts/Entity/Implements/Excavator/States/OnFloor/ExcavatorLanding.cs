namespace Unchord
{
    public class ExcavatorLanding : ExcavatorOnFloor
    {
        public override int idConstant => Excavator.c_st_BASIC_LANDING;

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Excavator.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            // instance.AllowHitFromBattleModule(true);
        }
    }
}