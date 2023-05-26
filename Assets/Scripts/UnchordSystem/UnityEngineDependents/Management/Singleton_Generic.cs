namespace Unchord
{
    public abstract class Singleton<T>
    where T : class, new()
    {
        private static T m_manager = null;

        public static T instance
        {
            get
            {
                if(m_manager == null)
                    m_manager = new T();
                return m_manager;
            }
        }
    }
}