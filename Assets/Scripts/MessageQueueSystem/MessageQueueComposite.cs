using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public sealed class MessageQueueComposite<T> : MessageQueueBase<T>
    {
        private List<MessageQueueBase<T>> m_queues;

        public MessageQueueComposite(int capacity = 1)
        {
            m_queues = new List<MessageQueueBase<T>>(capacity < 1 ? 1 : capacity);
        }

        public override void Clear()
        {
            for(int i = 0; i < m_queues.Count; ++i)
                m_queues[i]?.Clear();
        }

        public override void Enqueue(IMessage<T> message)
        {
            for(int i = 0; i < m_queues.Count; ++i)
                m_queues[i]?.Enqueue(message);
        }

        public override int Pass()
        {
            int sum = 0;

            for(int i = 0; i < m_queues.Count; ++i)
                sum += m_queues[i]?.Pass() ?? 0;

            return sum;
        }
    }
}