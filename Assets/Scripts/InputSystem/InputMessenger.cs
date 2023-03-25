namespace Unchord
{
    public class InputMessenger
    {
        private long m_msgCount = 0;

        public void Publish()
        {
            ++m_msgCount;
        }

        public long Get(bool bClearMsg = true)
        {
            long msgCount = m_msgCount;
            if(bClearMsg) this.Clear();
            return msgCount;
        }

        public void Clear()
        {
            m_msgCount = 0;
        }
    }
}