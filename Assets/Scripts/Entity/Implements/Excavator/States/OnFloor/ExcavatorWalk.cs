namespace Unchord
{
    public abstract class ExcavatorWalk : ExcavatorMove
    {
        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.vm.SetVelocityXY(0, 0);
        }
    }
}