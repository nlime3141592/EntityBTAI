using System;

namespace UnchordMetroidvania
{
    public abstract class PageNodeBT<T> : NodeBT<T>
    {
        protected PageNodeBT(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }
    }
} 