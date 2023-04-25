namespace Unchord
{
    public interface IStateCompositeBase : IStateBase
    {
        IStateBase this[int _index] { get; }
        int capacity { get; }
    }
}