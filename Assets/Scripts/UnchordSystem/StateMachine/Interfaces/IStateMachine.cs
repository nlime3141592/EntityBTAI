namespace Unchord
{
    public interface IStateMachine<T> : IStateMachineBase
    where T : class
    {
        T instance { get; set; }

        bool Add(IState<T> _state);
        bool Remove(IState<T> _state);
    }
}