namespace Unchord
{
    public class MantisSleep : MantisOnFloor
    {
        public override int idConstant => Mantis.c_st_SLEEP;

        public override void OnAggroBegin()
        {
            base.OnAggroBegin();

            instance.aggroAi.sensor.box.l = 200;
            instance.aggroAi.sensor.box.t = 200;
            instance.aggroAi.sensor.box.r = 200;
            instance.aggroAi.sensor.box.b = 200;
        }
    }
}