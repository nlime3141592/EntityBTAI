namespace UnchordMetroidvania
{
    public class PlayerIdleOnWall : PlayerOnWall
    {
        public PlayerIdleOnWall(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            float ix = config.instance.axisInput.x;

            if(ix == 0.0f)
                return InvokeResult.Failure;

            config.instance.FixConstraints(false, false);
            config.curTask = this;
            config.instance.velModule.SetVelocityXY(0.0f, 0.0f);

            return InvokeResult.Success;
        }
    }
}