namespace Unchord
{
    public abstract class MantisWalk : MantisMove
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            float dx = instance.aggroTargets[0].transform.position.x - instance.transform.position.x;

            if(dx < 0)
                instance.lookDir.x = Direction.Negative;
            else
                instance.lookDir.x = Direction.Positive;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.vm.SetVelocityXY(0, 0);
        }
    }
}