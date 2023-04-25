using System;

namespace Unchord
{
    public class StateMachineException : Exception
    {
        public IStateMachineBase machine { get; private set; }
        public Type instanceType { get; private set; }

        public StateMachineException(IStateMachineBase _machine, Type _instanceType)
        {
            machine = _machine;
            instanceType = _instanceType;
        }

        public StateMachineException(IStateMachineBase _machine, Type _instanceType, string message)
        : base(message)
        {
            machine = _machine;
            instanceType = _instanceType;
        }
    }
}