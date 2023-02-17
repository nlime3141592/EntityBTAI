namespace UnchordMetroidvania
{
    public abstract class ExcavatorWalk : ExcavatorMove
    {
        public ExcavatorWalk(Excavator _instance)
        : base(_instance)
        {
            
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            excavator.vm.SetVelocityXY(0, 0);
        }
    }
}