namespace UnchordMetroidvania
{
    public sealed class EntityEndOfAnimation<T> : EntityAnimationControl<T>
    where T : EntityBase
    {
        public EntityEndOfAnimation(T instance, AnimationController aController)
        : base(instance, aController)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(aController.bEndOfAnimation)
                return InvokeResult.Success;
            else
                return InvokeResult.Failure;
        }
    }
}