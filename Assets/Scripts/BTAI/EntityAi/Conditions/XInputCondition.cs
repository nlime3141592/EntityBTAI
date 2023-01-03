/*
using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class XInputCondition<EntityBase> : ConditionNodeBT<EntityBase>
    {
        protected XInputCondition(ConfigurationBT<EntityBase> config, int id, string name) : base(config, id, name) {}

        private sealed class m_BiggerThanZero : XInputCondition<EntityBase>
        {
            public m_BiggerThanZero(ConfigurationBT<EntityBase> config) : base(config, -1, "XInputCondition.m_BiggerThanZero") {}
            protected override InvokeResult p_Invoke() => config.instance.axisInput.x > 0.0f ? InvokeResult.Success : InvokeResult.Failure;
        }

        private sealed class m_SmallerThanZero : XInputCondition<EntityBase>
        {
            public m_SmallerThanZero(ConfigurationBT<EntityBase> config) : base(config, -1, "XInputCondition.m_SmallerThanZero") {}
            protected override InvokeResult p_Invoke() => config.instance.axisInput.x < 0.0f ? InvokeResult.Success : InvokeResult.Failure;
        }

        private sealed class m_EqualsZero : XInputCondition<EntityBase>
        {
            public m_EqualsZero(ConfigurationBT<EntityBase> config) : base(config, -1, "XInputCondition.m_EqualsZero") {}
            protected override InvokeResult p_Invoke() => config.instance.axisInput.x == 0.0f ? InvokeResult.Success : InvokeResult.Failure;
        }

        private sealed class m_NotEqualsZero : XInputCondition<EntityBase>
        {
            public m_NotEqualsZero(ConfigurationBT<EntityBase> config) : base(config, -1, "XInputCondition.m_NotEqualsZero") {}
            protected override InvokeResult p_Invoke() => config.instance.axisInput.x != 0.0f ? InvokeResult.Success : InvokeResult.Failure;
        }

        public static XInputCondition<EntityBase> BiggerThanZero(ConfigurationBT<EntityBase> config) => new m_BiggerThanZero(config);
        public static XInputCondition<EntityBase> SmallerThanZero(ConfigurationBT<EntityBase> config) => new m_SmallerThanZero(config);
        public static XInputCondition<EntityBase> EqualsZero(ConfigurationBT<EntityBase> config) => new m_EqualsZero(config);
        public static XInputCondition<EntityBase> NotEqualsZero(ConfigurationBT<EntityBase> config) => new m_NotEqualsZero(config);
    }
}
*/