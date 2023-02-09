using System;

namespace UnchordMetroidvania
{
    public class StateException : Exception
    {
        public StateException(string message)
        : base(message)
        {

        }

        public StateException(string message, Exception innerException)
        : base(message, innerException)
        {

        }
    }

    public class StateTransitException : StateException
    {
        public StateTransitException(string message)
        : base(message)
        {

        }

        public StateTransitException(string message, Exception innerException)
        : base(message, innerException)
        {

        }
    }
}