using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public abstract class CompositeNodeBT<T> : ControlNodeBT<T>
    {
        protected CompositeNodeBT(ConfigurationBT<T> config, int id, string name, int initCapacity)
        : base(config, id, name, initCapacity)
        {
            
        }
    }
}