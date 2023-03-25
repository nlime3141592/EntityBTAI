namespace Unchord
{
    public class MantisWalk : MantisMove
    {
        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.vm.SetVelocityXY(0, 0);
        }
    }
}