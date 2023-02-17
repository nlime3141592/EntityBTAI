namespace UnchordMetroidvania
{
    public class MantisWalk : MantisMove
    {
        public MantisWalk(Mantis _mantis)
        : base(_mantis)
        {
            
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            mantis.vm.SetVelocityXY(0, 0);
        }
    }
}