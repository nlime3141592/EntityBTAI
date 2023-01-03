namespace UnchordMetroidvania
{
    public abstract class DecoratorNodeBT<T> : BranchNodeBT<T>
    {
        protected NodeBT<T> child { get; private set; }

        protected DecoratorNodeBT(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        public NodeBT<T> Alloc(NodeBT<T> node)
        {
            NodeBT<T> prevNode = child;
            child = node;
            return prevNode;
        }

        public NodeBT<T> Dealloc()
        {
            return Alloc(null);
        }

        public override void ResetChild()
        {
            child.ResetNode();
        }
    }
}