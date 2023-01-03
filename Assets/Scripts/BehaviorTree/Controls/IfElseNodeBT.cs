using System;

namespace UnchordMetroidvania
{
    public class IfElseNodeBT<T> : ControlNodeBT<T>
    {
        internal IfElseNodeBT(ConfigurationBT<T> config, int id, string name, int initCapacity)
        : base(config, id, name, initCapacity)
        {
            if(initCapacity != 2 && initCapacity != 3)
                throw new ArgumentException("Invalid capacity.");
        }

        protected override InvokeResult p_Invoke()
        {
            InvokeResult iResult = InvokeResult.Failure;

            if(childIndex == 0)
            {
                iResult = children[0].Invoke();

                if(iResult == InvokeResult.Running)
                    return iResult;
                else if(iResult == InvokeResult.Success)
                    childIndex = 1;
                else if(iResult == InvokeResult.Failure)
                    childIndex = 2;
            }

            iResult = InvokeResult.Failure;

            if(childIndex > 0 && childIndex < children.Length)
                iResult = children[childIndex].Invoke();

            if(iResult != InvokeResult.Running)
            {
                ResetNode();
                ResetChild();
            }

            return iResult;
        }
    }
}