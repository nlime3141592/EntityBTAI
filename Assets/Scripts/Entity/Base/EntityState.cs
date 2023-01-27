namespace UnchordMetroidvania
{
    public abstract class EntityState<T> : UnchordState<T>
    where T : EntityBase
    {
        public EntityState(T _instance, int _id, string _name)
        : base(_instance, _id, _name)
        {

        }

        public sealed override void OnStateBegin()
        {
            base.OnStateBegin();
            p_OnStateBegin();
            instance.aController.Reset();
            p_OnChangeAnimation();
        }

        protected virtual void p_OnStateBegin() {}

        protected virtual void p_OnChangeAnimation()
        {
            instance.aController.ChangeAnimation(base.id);
        }

        public virtual void OnAnimationBegin() {}
        public virtual void OnActionBegin() {}
        public virtual void OnActionEnd() {}
        public virtual void OnAnimationEnd() {}
    }
}