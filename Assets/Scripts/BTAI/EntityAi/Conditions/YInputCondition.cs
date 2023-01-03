/*
using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class YInputCondition<EntityBase> : ConditionNodeBT<EntityBase>
    {
        protected YInputCondition(ConfigurationBT<EntityBase> config, int id, string name) : base(config, id, name) {}

        private sealed class m_BiggerThanZero : YInputCondition<EntityBase>
        {
            public m_BiggerThanZero(ConfigurationBT<EntityBase> config) : base(config, -1, "YInputCondition.m_BiggerThanZero") {}
            protected override InvokeResult p_Invoke() => config.instance.axisInput.y > 0.0f ? InvokeResult.Success : InvokeResult.Failure;
        }

        private sealed class m_SmallerThanZero : YInputCondition<EntityBase>
        {
            public m_SmallerThanZero(ConfigurationBT<EntityBase> config) : base(config, -1, "YInputCondition.m_SmallerThanZero") {}
            protected override InvokeResult p_Invoke() => config.instance.axisInput.y < 0.0f ? InvokeResult.Success : InvokeResult.Failure;
        }

        private sealed class m_EqualsZero : YInputCondition<EntityBase>
        {
            public m_EqualsZero(ConfigurationBT<EntityBase> config) : base(config, -1, "YInputCondition.m_EqualsZero") {}
            protected override InvokeResult p_Invoke() => config.instance.axisInput.y == 0.0f ? InvokeResult.Success : InvokeResult.Failure;
        }

        private sealed class m_NotEqualsZero : YInputCondition<EntityBase>
        {
            public m_NotEqualsZero(ConfigurationBT<EntityBase> config) : base(config, -1, "YInputCondition.m_NotEqualsZero") {}
            protected override InvokeResult p_Invoke() => config.instance.axisInput.y != 0.0f ? InvokeResult.Success : InvokeResult.Failure;
        }

        public static YInputCondition<EntityBase> BiggerThanZero(ConfigurationBT<EntityBase> config) => new m_BiggerThanZero(config);
        public static YInputCondition<EntityBase> SmallerThanZero(ConfigurationBT<EntityBase> config) => new m_SmallerThanZero(config);
        public static YInputCondition<EntityBase> EqualsZero(ConfigurationBT<EntityBase> config) => new m_EqualsZero(config);
        public static YInputCondition<EntityBase> NotEqualsZero(ConfigurationBT<EntityBase> config) => new m_NotEqualsZero(config);
    }
}
*/