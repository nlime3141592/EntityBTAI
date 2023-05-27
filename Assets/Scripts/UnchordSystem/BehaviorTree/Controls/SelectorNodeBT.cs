namespace UnchordMetroidvania
{
    public class SelectorNodeBT<T> : ControlNodeBT<T>
    {
        public SelectorNodeBT(T instance, int capacity)
        : base(instance, capacity)
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
                    return InvokeResult.Success;
                }
            }

            ResetNode();
            return InvokeResult.Failure;
        }
    }
}