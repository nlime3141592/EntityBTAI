namespace UnchordMetroidvania
{
    public class PlayerIdleLongOnFloor : PlayerIdleOnFloor
    {
        public PlayerIdleLongOnFloor(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.axisInput.x != 0 || config.instance.axisInput.y != 0)
            {
                return InvokeResult.Failure;
            }
            else
            {
                config.curTask = this;
                return base.p_Invoke();
            }
        }
    }
}