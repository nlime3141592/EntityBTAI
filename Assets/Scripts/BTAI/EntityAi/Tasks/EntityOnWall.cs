namespace UnchordMetroidvania
{
    public abstract class EntityOnWall<T_Config> : TaskNodeBT<T_Config>
    where T_Config : IEntityOnWallConfig
    {
        protected EntityOnWall(T_Config config, int id, string name)
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