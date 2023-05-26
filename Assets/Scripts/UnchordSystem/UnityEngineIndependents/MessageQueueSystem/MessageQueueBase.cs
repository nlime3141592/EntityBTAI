using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public abstract class MessageQueueBase<T>
    {
        public abstract void Clear();
        public abstract void Enqueue(IMessage<T> message);
        public abstract int Pass();
    }
}