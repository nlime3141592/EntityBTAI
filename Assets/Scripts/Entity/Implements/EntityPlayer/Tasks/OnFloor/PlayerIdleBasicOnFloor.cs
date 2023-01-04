namespace UnchordMetroidvania
{
    public class PlayerIdleBasicOnFloor : PlayerIdleOnFloor
    {
        public int maxFrame = 900;

        public PlayerIdleBasicOnFloor(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.axisInput.x != 0 || config.instance.axisInput.y != 0)
            {
                return InvokeResult.Failure;
            }
            else if(base.lastFps - base.beginFps < maxFrame)
            {
                config.curTask = this;
                return base.p_Invoke();
            }
            else
            {
                return InvokeResult.Failure;
            }
        }
    }
}