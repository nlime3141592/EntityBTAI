namespace Unchord
{
    public interface IStateMachine<T> : IStateMachineBase
    where T : class
    {
        T instance { get; set; }
    }
}