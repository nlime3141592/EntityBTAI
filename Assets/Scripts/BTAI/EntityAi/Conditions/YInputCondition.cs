namespace UnchordMetroidvania
{
    public abstract class YInputCondition<T_Config> : ConditionNodeBT<T_Config>
    where T_Config : IEntityInputConfig
    {
        protected YInputCondition(T_Config config, int id, string name) : base(config, id, name) {}

        private sealed class m_BiggerThanZero<T_InConfig> : YInputCondition<T_InConfig>
        where T_InConfig : IEntityInputConfig
        {
            public m_BiggerThanZero(T_InConfig config) : base(config, -1, "YInputCondition.m_BiggerThanZero") {}
            public override InvokeResult Invoke() => p_config.yInput > 0.0f ? InvokeResult.SUCCESS : InvokeResult.FAIL;
        }

        private sealed class m_SmallerThanZero<T_InConfig> : YInputCondition<T_InConfig>
        where T_InConfig : IEntityInputConfig
        {
            public m_SmallerThanZero(T_InConfig config) : base(config, -1, "YInputCondition.m_SmallerThanZero") {}
            public override InvokeResult Invoke() => p_config.yInput < 0.0f ? InvokeResult.SUCCESS : InvokeResult.FAIL;
        }

        private sealed class m_EqualsZero<T_InConfig> : YInputCondition<T_InConfig>
        where T_InConfig : IEntityInputConfig
        {
            public m_EqualsZero(T_InConfig config) : base(config, -1, "YInputCondition.m_EqualsZero") {}
            public override InvokeResult Invoke() => p_config.yInput == 0.0f ? InvokeResult.SUCCESS : InvokeResult.FAIL;
        }

        public static YInputCondition<T_Config> BiggerThanZero(T_Config config) => new m_BiggerThanZero<T_Config>(config);
        public static YInputCondition<T_Config> SmallerThanZero(T_Config config) => new m_SmallerThanZero<T_Config>(config);
        public static YInputCondition<T_Config> EqualsZero(T_Config config) => new m_EqualsZero<T_Config>(config);
    }
}