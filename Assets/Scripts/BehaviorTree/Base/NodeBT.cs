using System;

namespace UnchordMetroidvania
{
    public abstract class NodeBT<T>
    {
        public ConfigurationBT<T> config { get; private set; }
        public int id { get; private set; }
        public string name { get; private set; }

        public bool bActive = true;
        public InvokeResult inactiveResult = InvokeResult.Failure;

        protected NodeBT(ConfigurationBT<T> config, int id, string name)
        {
            this.config = config;
            this.id = id;
            this.name = name;
        }

        public InvokeResult Invoke()
        {
            if(!bActive)
                return inactiveResult;

            p_OnPreInvokeNode();
            InvokeResult iResult = p_Invoke();
            if(iResult == InvokeResult.Running)
                p_OnRunning();
            else if(iResult == InvokeResult.Success)
                p_OnSuccess();
            else if(iResult == InvokeResult.Failure)
                p_OnFailure();
            p_OnPostInvokeNode();
            return iResult;
        }

        public virtual void ResetNode() {}
        protected virtual void p_OnPreInvokeNode() {}
        protected abstract InvokeResult p_Invoke();
        protected virtual void p_OnRunning() {}
        protected virtual void p_OnSuccess() {}
        protected virtual void p_OnFailure() {}
        protected virtual void p_OnPostInvokeNode() {}
    }
}