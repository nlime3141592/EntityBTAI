using System.Collections.Generic;

namespace Unchord
{
    public interface IStateMachine<T> : IStateMachineRemote
    where T : class
    {
        void Begin(T _instance, IState<T> _stateTree, int _state);
        void Change(int _state);
    }
}