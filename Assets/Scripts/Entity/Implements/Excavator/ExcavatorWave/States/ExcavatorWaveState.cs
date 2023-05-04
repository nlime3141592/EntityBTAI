namespace Unchord
{
    public abstract class ExcavatorWaveState : EntityState<ExcavatorWave>
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.senseData.OnFixedUpdate(instance);
        }
    }
}