using System;

namespace UnchordMetroidvania
{
    public abstract class NodeBT<T>
    {
        public ConfigurationBT<T> config { get; private set; }
        public int id { get; private set; }
        public string name { get; private set; }

        public long beginFps { get; private set; } = -1;
        public long lastFps { get; private set; } = -1;

        public bool bActive = true;
        public InvokeResult inactiveResult = InvokeResult.Failure;

        protected NodeBT(ConfigurationBT<T> config, int id, string name)
        {
            if(config == null)
                throw new ArgumentNullException("Instance cannot be null.");

            this.config = config;
            this.id = id;
            this.name = name;
        }

        public InvokeResult Invoke()
        {
            if(!bActive)
                return inactiveResult;

            long curFps = config.curFps;
            long dF = curFps - lastFps;

            if(dF > 1) m_OnBeginNode(curFps);
            if(dF > 0) lastFps = curFps;

            return p_Invoke();
        }

        public virtual void ResetNode()
        {

        }

        protected virtual void p_OnBeginNode()
        {
            
        }

        protected abstract InvokeResult p_Invoke();

        private void m_OnBeginNode(long curFps)
        {
            beginFps = curFps;
            p_OnBeginNode();
        }
    }
}