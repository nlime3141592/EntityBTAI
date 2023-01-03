using System;

namespace UnchordMetroidvania
{
    public abstract class TaskNodeBT<T> : NodeBT<T>
    {
        protected TaskNodeBT(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        public override void ResetNode()
        {
            base.ResetNode();
        }
    }
}