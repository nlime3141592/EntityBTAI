namespace UnchordMetroidvania
{
    public sealed class EntityChangeOfActionPhase<T> : EntityAnimationControl<T>
    where T : EntityBase
    {
        public int phase;

        public EntityChangeOfActionPhase(T instance, AnimationController aController)
        : base(instance, aController)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(phase < 0)
                return InvokeResult.Failure;
            else
                return InvokeResult.Success;
        }

        protected override void p_OnSuccess()
        {
            aController.ChangeActionPhase(phase);
        }
    }
}