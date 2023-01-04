namespace UnchordMetroidvania
{
    public class PlayerHeadUp : PlayerIdleOnFloor
    {
        public int camUpFrame = 100;
        public bool bCamUp { get; private set; }

        public PlayerHeadUp(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.axisInput.y > 0)
            {
                config.curTask = this;
                bCamUp = base.lastFps - base.beginFps >= camUpFrame;
                return base.p_Invoke();
            }
            else
            {
                return InvokeResult.Failure;
            }
        }
    }
}