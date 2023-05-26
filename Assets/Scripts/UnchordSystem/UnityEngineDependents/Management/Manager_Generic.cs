namespace Unchord
{
    public abstract class Manager<T> : Singleton<T>
    where T : class, new()
    {

    }
}