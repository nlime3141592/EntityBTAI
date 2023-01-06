namespace UnchordMetroidvania
{
    public class PlayerJumpBegin : PlayerCondition
    {
        public PlayerJumpBegin(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {
            
        }

        protected override InvokeResult p_Invoke()
        {
            bool bBegin = config.instance.jumpBeginMessenger.Get() > 0;

            if(bBegin)
                return InvokeResult.Success;
            else
                return InvokeResult.Failure;
        }
    }
}