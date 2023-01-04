namespace UnchordMetroidvania
{
    public class PlayerOnFloorPage : PageNodeBT<EntityPlayer>
    {
        public readonly PlayerIdleBasicOnFloor idleBasic;
        public readonly PlayerIdleLongOnFloor idleLong;
        public readonly PlayerSit sit;
        public readonly PlayerHeadUp headUp;
        public readonly PlayerMoveOnFloor walk;

        private SelectorNodeBT<EntityPlayer> m_pageRoot;

        public PlayerOnFloorPage(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {
            int allocIndex = -1;

            idleBasic = new PlayerIdleBasicOnFloor(config, 0, "IdleBasicOnFloor");
            idleLong = new PlayerIdleLongOnFloor(config, 1, "IdleLongOnFloor");
            sit = new PlayerSit(config, 2, "Sit");
            headUp = new PlayerHeadUp(config, 3, "HeadUp");
            walk = new PlayerMoveOnFloor(config, 4, "Walk");

            m_pageRoot = BehaviorTree.Selector<EntityPlayer>(config, id, name, 5);
            allocIndex = -1;
            m_pageRoot.Alloc(++allocIndex, walk);
            m_pageRoot.Alloc(++allocIndex, sit);
            m_pageRoot.Alloc(++allocIndex, headUp);
            m_pageRoot.Alloc(++allocIndex, idleBasic);
            m_pageRoot.Alloc(++allocIndex, idleLong);
        }

        protected override InvokeResult p_Invoke()
        {
            // Debug here.
            walk.speedStat = config.instance.baseMoveSpeed;

            return m_pageRoot.Invoke();
        }
    }
}