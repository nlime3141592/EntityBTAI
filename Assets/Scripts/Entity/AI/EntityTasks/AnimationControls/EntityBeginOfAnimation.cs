namespace UnchordMetroidvania
{
    public sealed class EntityBeginOfAnimation<T> : EntityAnimationControl<T>
    where T : EntityBase
    {
        public EntityBeginOfAnimation(T instance, AnimationController aController)
        : base(instance, aController)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(aController.bBeginOfAnimation)
                return InvokeResult.Success;
            else
                return InvokeResult.Running;
        }
    }
}