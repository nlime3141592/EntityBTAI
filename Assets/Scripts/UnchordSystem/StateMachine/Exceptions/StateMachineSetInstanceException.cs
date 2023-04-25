using System;

namespace Unchord
{
    public class StateMachineSetInstanceException : StateMachineException
    {
        public StateMachineSetInstanceException(IStateMachineBase _machine, Type _instanceType)
        : base(
            _machine,
            _instanceType,
            string.Format($"Unchord.IStateMachine<{_instanceType.Name}>.machine can't be set/change when state machine is running.")
            )
        {

        }
    }
}