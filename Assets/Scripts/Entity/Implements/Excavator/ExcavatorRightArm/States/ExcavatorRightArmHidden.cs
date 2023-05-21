namespace Unchord
{
    public class ExcavatorRightArmHidden : ExcavatorRightArmState
    {
        public override int idConstant => ExcavatorRightArm.c_st_HIDDEN;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.gameObject.SetActive(false);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.nextState == ExcavatorRightArm.c_st_MOVE)
                return instance.nextState;

            return MachineConstant.c_lt_PASS;
        }
    }
}