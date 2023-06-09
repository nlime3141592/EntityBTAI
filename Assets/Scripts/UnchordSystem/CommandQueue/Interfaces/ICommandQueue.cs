namespace Unchord
{
    public interface ICommandQueue : ICommand
    {
        ICommandQueue Enqueue(ICommand _cmd);
        ICommandQueue Enqueue(Command _cmd);

        int Count { get; }
        int CountBegin { get; }
    }
}