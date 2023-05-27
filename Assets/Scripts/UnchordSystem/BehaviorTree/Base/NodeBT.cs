using System;

namespace UnchordMetroidvania
{
    public abstract class NodeBT<T>
    {
        protected T instance { get; private set; }
        public bool bActive = true;
        public InvokeResult inactiveResult = InvokeResult.Failure;
        public int id = 0;

        public NodeBT(T instance)
        {
            this.instance = instance;
        }

        public InvokeResult Invoke()
        {
            if(!bActive)
                return inactiveResult;

            p_OnInvokeBegin();

            InvokeResult iResult = p_Invoke();

            p_OnInvokeEnd();

            if(iResult == InvokeResult.Running) p_OnRunning();
            else if(iResult == InvokeResult.Success) p_OnSuccess();
            else if(iResult == InvokeResult.Failure) p_OnFailure();

            return iResult;
        }

        public virtual void ResetNode()
        {
            Console.Write("{0:x2} ", id);
        }

        protected virtual void p_OnInvokeBegin() {}

        protected abstract InvokeResult p_Invoke();

        protected virtual void p_OnRunning() {}
        protected virtual void p_OnSuccess() {}
        protected virtual void p_OnFailure() {}
        
        protected virtual void p_OnInvokeEnd() {}
    }
}