namespace UnchordMetroidvania
{
    public abstract class NodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        public readonly T_ConfigurationBT p_config;
        public int id { get; protected set; }
        public string name { get; protected set; }

        public bool bSkipped { get; set; }
        protected long p_invokedFps { get; private set; }
        protected long p_lastFps { get; private set; }

        protected NodeBT(T_ConfigurationBT config, int id, string name)
        {
            this.p_config = config;
            this.name = name;
            this.id = id;

            bSkipped = false;
            p_lastFps = -1;
        }

        public virtual InvokeResult Invoke()
        {
            if(p_lastFps == -1)
            {
                p_lastFps = p_config.curFps;
                return InvokeResult.SUCCESS;
            }

            long dF = p_config.curFps - p_lastFps;
            p_lastFps = p_config.curFps;

            if(dF == 1) // NOTE: Node 실행이 연속으로 이루어짐.
                return InvokeResult.RUNNING;

            p_invokedFps = p_config.curFps;

            if(dF < 1)
                return InvokeResult.FAIL;
            else
                return InvokeResult.SUCCESS;
        }
    }
}