namespace UnchordMetroidvania
{
    public class EntityIdle<T> : EntityTask<T>
    where T : EntityBase
    {
        public EntityIdle(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            config.instance.vm.SetVelocityXY(0.0f, 0.0f);
            return InvokeResult.Running;
        }
    }
}