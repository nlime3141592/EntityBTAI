namespace UnchordMetroidvania
{
    public class PlayerJumpCancel : PlayerCondition
    {
        public PlayerJumpCancel(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {
            
        }

        protected override InvokeResult p_Invoke()
        {
            bool bCancel = config.instance.jumpCancelMessenger.Get() > 0;

            if(bCancel)
                return InvokeResult.Success;
            else
                return InvokeResult.Failure;
        }
    }
}