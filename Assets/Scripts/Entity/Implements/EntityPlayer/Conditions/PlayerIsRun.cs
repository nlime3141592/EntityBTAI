namespace UnchordMetroidvania
{
    public class PlayerIsRun : ConditionNodeBT<EntityPlayer>
    {
        public PlayerIsRun(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.bIsRun)
                return InvokeResult.Success;
            else
                return InvokeResult.Failure;
        }
    }
}