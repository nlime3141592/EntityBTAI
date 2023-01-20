namespace UnchordMetroidvania
{
    public abstract class EntityAnimationControl<T> : TaskNodeBT<T>
    where T : EntityBase
    {
        protected AnimationController aController { get; private set; }

        public EntityAnimationControl(T instance, AnimationController aController)
        : base(instance)
        {
            this.aController = aController;
        }
    }
}