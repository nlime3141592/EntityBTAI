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

        public override void OnAggroBegin()
        {
            base.OnAggroBegin();

            instance.aggroAi.sensor.box.l = 200;
            instance.aggroAi.sensor.box.t = 200;
            instance.aggroAi.sensor.box.r = 200;
            instance.aggroAi.sensor.box.b = 200;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aggroAi.bAggro)
                return Mantis.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }
    }
}