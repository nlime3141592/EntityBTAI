namespace Unchord
{
    public abstract class ExcavatorMove : ExcavatorOnFloor
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(false, false);

            instance.bFixedLookDirByAxis.x = true;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Excavator.c_st_IDLE;
            // else if(instance.senseData.bOnWallFront) return ExcavatorFsm.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.bFixedLookDirByAxis.x = false;
        }
    }
}