namespace UnchordMetroidvania
{
    public abstract class DecoratorNodeBT<T_ConfigurationBT> : BranchNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        protected NodeBT<T_ConfigurationBT> p_internalNode { get; private set; }

        protected DecoratorNodeBT(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node)
        : base(config, node.id, node.name)
        {
            p_internalNode = node;
        }

        public NodeBT<T_ConfigurationBT> Set(NodeBT<T_ConfigurationBT> node)
        {
            NodeBT<T_ConfigurationBT> prevNode = p_internalNode;
            p_internalNode = node;
            base.id = node.id;
            base.name = node.name;
            return prevNode;
        }
    }
}