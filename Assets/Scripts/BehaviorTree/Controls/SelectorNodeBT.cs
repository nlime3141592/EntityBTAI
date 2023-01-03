namespace UnchordMetroidvania
{
    public class SelectorNodeBT<T> : CompositeNodeBT<T>
    {
        internal SelectorNodeBT(ConfigurationBT<T> config, int id, string name, int initCapacity)
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
                else if(iResult == InvokeResult.Success)
                {
                    ResetNode();
                    ResetChild();
                    return InvokeResult.Success;
                }
            }

            ResetNode();
            ResetChild();
            return InvokeResult.Failure;
        }
    }
}