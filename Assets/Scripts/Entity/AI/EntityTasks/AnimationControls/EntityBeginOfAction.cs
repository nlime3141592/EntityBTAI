namespace UnchordMetroidvania
{
    public sealed class EntityBeginOfAction<T> : EntityAnimationControl<T>
    where T : EntityBase
    {
        public EntityBeginOfAction(T instance, AnimationController aController)
        : base(instance, aController)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(aController.bBeginOfAction)
                return InvokeResult.Success;
            else
                return InvokeResult.Running;
        }
    }
}