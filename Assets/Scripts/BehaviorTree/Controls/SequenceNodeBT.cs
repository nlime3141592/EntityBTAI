namespace UnchordMetroidvania
{
    public class SequenceNodeBT<T> : CompositeNodeBT<T>
    {
        internal SequenceNodeBT(ConfigurationBT<T> config, int id, string name, int initCapacity)
        : base(config, id, name, initCapacity)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            for(int i = childIndex; i < children.Length; ++i)
            {
                InvokeResult iResult = children[i].Invoke();

                if(iResult == InvokeResult.Running)
                {
                    childIndex = i;
                    return InvokeResult.Running;
                }
                else if(iResult == InvokeResult.Failure)
                {
                    ResetNode();
                    return InvokeResult.Failure;
                }
            }

            ResetNode();
            return InvokeResult.Success;
        }
    }
}