using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public abstract class ControlNodeBT<T_ConfigurationBT> : BranchNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        protected List<NodeBT<T_ConfigurationBT>> p_children { get; private set; }
        protected int p_childIndex = 0;

        protected ControlNodeBT(T_ConfigurationBT config, int id, string name, int initCapacity)
        : base(config, id, name)
        {
            p_children = new List<NodeBT<T_ConfigurationBT>>(initCapacity);
            for(int i = 0; i < initCapacity; ++i) p_children.Add(null);
            p_childIndex = 0;
        }

        public void Set(int index, NodeBT<T_ConfigurationBT> node)
        {
            p_children[index] = node;
        }

        public NodeBT<T_ConfigurationBT> Reset(int index)
        {
            NodeBT<T_ConfigurationBT> node = p_children[index];
            p_children[index] = null;
            return node;
        }

        public override InvokeResult Invoke()
        {
            InvokeResult iResult = base.Invoke();

            if(iResult == InvokeResult.SUCCESS)
                p_childIndex = 0;

            return iResult;
        }
    }
}