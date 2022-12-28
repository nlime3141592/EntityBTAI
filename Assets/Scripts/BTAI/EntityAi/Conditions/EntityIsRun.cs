namespace UnchordMetroidvania
{
    public class EntityIsRun<T_Config> : ConditionNodeBT<T_Config>
    where T_Config : IEntityRunConfig
    {
        public EntityIsRun(T_Config config)
        : base(config, -1, "EntityIsRun")
        {

        }

        public override InvokeResult Invoke()
        {
            if(p_config.isRun)
                return InvokeResult.SUCCESS;
            else
                return InvokeResult.FAIL;
        }
    }
}