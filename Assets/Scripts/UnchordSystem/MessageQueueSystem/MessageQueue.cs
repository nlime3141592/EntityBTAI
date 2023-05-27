using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public sealed class MessageQueue<T> : MessageQueueBase<T>
    {
        private Queue<IMessage<T>> m_messages;
        private bool m_bCanPass = false;

        public MessageQueue(int capacity = 1)
        {
            m_messages = new Queue<IMessage<T>>(capacity < 1 ? 1 : capacity);
            m_bCanPass = false;
        }

        public override void Clear()
        {
            m_messages.Clear();
        }

        public override void Enqueue(IMessage<T> message)
        {
            m_messages.Enqueue(message);
        }

        public override int Pass()
        {
            if(!m_bCanPass || m_messages.Count == 0)
                return 0;

            m_bCanPass = false;
            m_messages.Dequeue();
            return 1;
        }

        public bool Ignore()
        {
            if(m_messages.Count == 0)
                return false;

            m_bCanPass = false;
            m_messages.Dequeue();
            return true;
        }

        public IMessage<T> Peek()
        {
            m_bCanPass = true;
            return m_messages.Peek() ?? null;
        }
    }
}