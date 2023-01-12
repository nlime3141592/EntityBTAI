namespace UnchordMetroidvania
{
    public class SequenceNodeBT<T> : ControlNodeBT<T>
    {
        public SequenceNodeBT(T instance, int capacity)
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