namespace UnchordMetroidvania
{
    public interface IMessage<T>
    {
        T instance { get; }
    }
}