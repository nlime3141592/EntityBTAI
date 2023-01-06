using System;

namespace UnchordMetroidvania
{
    public abstract class ControlNodeBT<T> : BranchNodeBT<T>
    {
        protected NodeBT<T>[] children { get; private set; }
        protected int childIndex = 0;

        protected ControlNodeBT(ConfigurationBT<T> config, int id, string name, int initCapacity)
        : base(config, id, name)
        {
            children = new NodeBT<T>[initCapacity];
        }

        public void Alloc(int index, NodeBT<T> node)
        {
            children[index] = node;
        }

        public NodeBT<T> Dealloc(int index)
        {
            NodeBT<T> prevNode = children[index];
            children[index] = null;
            return prevNode;
        }

        public override void ResetNode()
        {
            base.ResetNode();

            childIndex = 0;
            for(int i = 0; i < children.Length; ++i)
                children[i].ResetNode();
        }
    }
}