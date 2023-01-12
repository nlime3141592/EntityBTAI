namespace UnchordMetroidvania
{
    public abstract class IfNodeBT<T> : ControlNodeBT<T>
    {
        protected IfNodeBT(T instance, int capacity)
        : base(instance, capacity)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            InvokeResult iResult = InvokeResult.Failure;

            if(childIndex == 0)
            {
                iResult = children[childIndex]?.Invoke() ?? InvokeResult.Failure;

                if(iResult == InvokeResult.Running)
                    return iResult;
                else if(iResult == InvokeResult.Success)
                    childIndex = 1;
                else if(iResult == InvokeResult.Failure)
                    childIndex = 2;
            }

            iResult = InvokeResult.Failure;

            if(childIndex > 0 && childIndex < children.Length)
                iResult = children[childIndex]?.Invoke() ?? InvokeResult.Failure;

            if(iResult != InvokeResult.Running)
                ResetNode();

            return iResult;
        }
    }
}