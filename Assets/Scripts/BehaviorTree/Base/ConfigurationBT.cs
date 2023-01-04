using System;

namespace UnchordMetroidvania
{
    public class ConfigurationBT<T>
    {
        public long prevFps;
        public long curFps;

        public TaskNodeBT<T> prevTask;
        public TaskNodeBT<T> curTask;

        public T instance { get; private set; }

        public ConfigurationBT(T instance)
        {
            if(instance == null)
                throw new ArgumentNullException("Instance cannot be null.");

            this.instance = instance;
        }
    }
}