namespace Unchord
{
    public class ExcavatorWaveIdle : ExcavatorWaveState
    {
        public override int idConstant => ExcavatorWave.c_st_IDLE;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(true, false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(!instance.senseData.datFloor.bOnDetected)
                return;

            instance.vm.SetVelocityY(-100.0f);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bInstanceReady)
            {
                if(!instance.senseData.datFloor.bOnDetected)
                    return MachineConstant.c_st_MACHINE_OFF;
                else if(instance.senseData.datFloor.bOnHit)
                    return ExcavatorWave.c_st_SHAKE;
            }

            return MachineConstant.c_lt_PASS;
        }
    }
}