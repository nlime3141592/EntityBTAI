namespace UnchordMetroidvania
{
    public class PlayerSit : PlayerIdleOnFloor
    {
        public int camDownFrame = 100;
        public bool bCamDown { get; private set; }

        public PlayerSit(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.axisInput.y < 0)
            {
                config.instance.FixConstraints(true, false);
                config.curTask = this;
                // bCamDown = base.lastFps - base.beginFps >= camDownFrame;
                return base.p_Invoke();
            }
            else
            {
                return InvokeResult.Failure;
            }
        }
    }
}