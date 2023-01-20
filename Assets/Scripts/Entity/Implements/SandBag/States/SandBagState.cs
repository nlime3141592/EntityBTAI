namespace UnchordMetroidvania
{
    public abstract class SandBagState : EntityStateBT<SandBag>
    {
        public SandBagState(SandBag instance, AnimationController aController)
        : base(instance, aController)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            aController.Reset();
        }
    }
}