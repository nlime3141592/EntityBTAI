namespace Unchord
{
    public interface IStateComposite<T> : IState<T>, IStateCompositeBase
    where T : class
    {
        new IState<T> this[int _index] { get; set; }
    }
}