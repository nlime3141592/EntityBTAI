namespace Unchord
{
    public class MantisSleep : MantisOnFloor
    {
        public override int idConstant => Mantis.c_st_SLEEP;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.vm.SetVelocityY(-1.0f);
        }

        public override void OnAggroBegin(SEH_EntityAggression _aggModule)
        {
            base.OnAggroBegin(_aggModule);

            _aggModule.boxSensor.box.l = 200;
            _aggModule.boxSensor.box.t = 200;
            _aggModule.boxSensor.box.r = 200;
            _aggModule.boxSensor.box.b = 200;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bAggro)
                return Mantis.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }
    }
}