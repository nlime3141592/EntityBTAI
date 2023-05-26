using System;

namespace UnchordMetroidvania
{
    public abstract class CompositeNodeBT<T> : BranchNodeBT<T>
    {
        public NodeBT<T> this[int index]
        {
            get
            {
                try { return children[index]; }
                catch(Exception exception) { throw exception; }
            }
            set
            {
                try { children[index] = value; }
                catch(Exception exception) { throw exception; }
            }
        }

        protected NodeBT<T>[] children { get; private set; }
        protected int childIndex = 0;

        public CompositeNodeBT(T instance, int capacity = 2)
        : base(instance)
        {
            if(capacity < 1)
                capacity = 1;

            children = new NodeBT<T>[capacity];
        }

        public override void ResetNode()
        {
            childIndex = 0;

            base.ResetNode();
        }
    }
}