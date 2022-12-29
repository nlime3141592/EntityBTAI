namespace UnchordMetroidvania
{
    public abstract class EntityOnAir<T_Config> : TaskNodeBT<T_Config>
    where T_Config : IEntityMovementConfig
    {
        protected EntityOnAir(T_Config config, int id, string name)
        : base(config, id, name)
        {

        }

        public override InvokeResult Invoke()
        {
            base.Invoke();
            p_config.currentState = this.id;
            return InvokeResult.SUCCESS;
        }
    }
}