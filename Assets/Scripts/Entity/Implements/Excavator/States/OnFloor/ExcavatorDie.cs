namespace Unchord
{
    public class ExcavatorDie : ExcavatorOnFloor
    {
        public override int idConstant => Excavator.c_st_DIE;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.IgnoreBattleTrigger(null, false); // TODO: 배틀 트리거를 넣어줘야 함.
            instance.armObj.SetActive(false);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return MachineConstant.c_st_MACHINE_OFF;

            return MachineConstant.c_lt_PASS;
        }
    }
}