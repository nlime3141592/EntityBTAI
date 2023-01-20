namespace UnchordMetroidvania
{
    public sealed class EntityChangeOfAnimation<T> : EntityAnimationControl<T>
    where T : EntityBase
    {
        public int state;

        public EntityChangeOfAnimation(T instance, AnimationController aController)
        : base(instance, aController)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(state < 0)
                return InvokeResult.Failure;
            else
                return InvokeResult.Success;
        }

        protected override void p_OnSuccess()
        {
            aController.ChangeAnimation(state);
        }
    }
}