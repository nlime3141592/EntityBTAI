namespace Unchord
{
    public interface ICompositeState<T> : IState<T>
    where T : class
    {
        IState<T> this[int _index] { get; set; }
        int capacity { get; }
    }
}