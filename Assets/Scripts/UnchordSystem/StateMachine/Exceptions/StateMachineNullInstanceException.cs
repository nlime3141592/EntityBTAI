using System;

namespace Unchord
{
    public class StateMachineNullInstanceException : StateMachineException
    {
        public StateMachineNullInstanceException(IStateMachineBase _machine, Type _instanceType)
        : base(
            _machine,
            _instanceType,
            string.Format($"Unchord.IStateMachine<{_instanceType.Name}>.machine is NULL.")
            )
        {

        }
    }
}