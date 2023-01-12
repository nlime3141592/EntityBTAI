namespace UnchordMetroidvania
{
    public class EntityIdle<T> : EntityTask<T>
    where T : EntityBase
    {
        public EntityIdle(T instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            instance.vm.SetVelocityXY(0.0f, 0.0f);
            return InvokeResult.Running;
        }
    }
}