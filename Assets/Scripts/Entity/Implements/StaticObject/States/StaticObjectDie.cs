namespace Unchord
{
    public class StaticObjectDie : StaticObjectState
    {
        public override int idConstant => StaticObject.c_st_DIE;

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
            else if(instance.bEndOfAnimation)
                return MachineConstant.c_st_MACHINE_OFF;

            return MachineConstant.c_lt_PASS;
        }
    }
}