namespace UnchordMetroidvania
{
    public sealed class EntityEndOfAction<T> : EntityAnimationControl<T>
    where T : EntityBase
    {
        public EntityEndOfAction(T instance, AnimationController aController)
        : base(instance, aController)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(aController.bEndOfAction)
                return InvokeResult.Success;
            else
                return InvokeResult.Failure;
        }
    }

}