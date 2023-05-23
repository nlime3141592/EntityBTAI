namespace Unchord
{
    public class StaticObjectIdle : StaticObjectState
    {
        public override int idConstant => StaticObject.c_st_IDLE;

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
            else if(!instance.senseData.datFloorL.bOnHit && !instance.senseData.datFloorR.bOnHit)
                return StaticObject.c_st_FREE_FALL;

            return MachineConstant.c_lt_PASS;
        }
    }
}