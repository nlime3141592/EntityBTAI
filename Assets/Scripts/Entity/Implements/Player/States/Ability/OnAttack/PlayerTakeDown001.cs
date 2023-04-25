namespace Unchord
{
    public class PlayerTakeDown001 : PlayerTakeDownBase
    {
        public override int idConstant => Player.c_st_TAKE_DOWN_001;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(true, true);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Player.c_st_TAKE_DOWN_002;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.vm.FreezePosition(false, false);
        }
    }
}