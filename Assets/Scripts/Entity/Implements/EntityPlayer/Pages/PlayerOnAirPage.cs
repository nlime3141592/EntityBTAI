namespace UnchordMetroidvania
{
    public class PlayerOnAirPage : PageNodeBT<EntityPlayer>
    {
        public readonly PlayerFreeFall freeFall;
        public readonly PlayerGliding gliding;

        private SelectorNodeBT<EntityPlayer> m_pageRoot;

        public PlayerOnAirPage(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {
            freeFall = new PlayerFreeFall(config, 0, "FreeFall");
            gliding = new PlayerGliding(config, 1, "Gliding");

            m_pageRoot = BehaviorTree.Selector<EntityPlayer>(config, id, name, 2);

            m_pageRoot.Alloc(0, gliding);
            m_pageRoot.Alloc(1, freeFall);
        }

        protected override InvokeResult p_Invoke()
        {
            freeFall.speedStatX = config.instance.bIsRun ? config.instance.runSpeed : config.instance.baseMoveSpeed;
            // speedStatY 기본값: -12.0f
            freeFall.gravityStatY = config.instance.baseGravity;

            gliding.speedStatX = config.instance.bIsRun ? config.instance.runSpeed : config.instance.baseMoveSpeed;
            // speedStatY 기본값: -3.0f
            gliding.gravityStatY = config.instance.glidingGravity;

            return m_pageRoot.Invoke();
        }
    }
}