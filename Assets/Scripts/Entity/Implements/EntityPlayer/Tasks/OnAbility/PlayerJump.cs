namespace UnchordMetroidvania
{
    public abstract class PlayerJump
    {
        public PlayerJump(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }
    }

    public class PlayerJumpOnFloor
    {
        public Stat jumpSpeedStatY;

        private int leftJumpFrame;

        public PlayerJumpOnFloor(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.bOnJumpBegin)
            {
                leftJumpFrame = config.instance.maxJumpFrame;
                config.instance.bOnJumpBegin = false;
            }
            float vy = 0;
            config.curTask = this;
            return InvokeResult.Failure;
        }
    }
}