using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public abstract class CompositeNodeBT<T_ConfigurationBT> : ControlNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        protected CompositeNodeBT(T_ConfigurationBT config, int id, string name, int initCapacity)
        : base(config, id, name, initCapacity)
        {
            
        }
    }
}