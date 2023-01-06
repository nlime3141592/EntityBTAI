namespace UnchordMetroidvania
{
    public class PlayerOnFloorPage : PageNodeBT<EntityPlayer>
    {
        public readonly PlayerIdleBasicOnFloor idleBasic;
        public readonly PlayerIdleLongOnFloor idleLong;
        public readonly PlayerSit sit;
        public readonly PlayerHeadUp headUp;
        public readonly PlayerMoveOnFloor walk;
        public readonly PlayerMoveOnFloor run;

        private SelectorNodeBT<EntityPlayer> m_pageRoot;

        private IfElseNodeBT<EntityPlayer> m_isRun;
        private PlayerIsRun m_bIsRun;

        public PlayerOnFloorPage(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {
            int allocIndex = -1;

            idleBasic = new PlayerIdleBasicOnFloor(config, 100, "IdleBasicOnFloor");
            idleLong = new PlayerIdleLongOnFloor(config, 101, "IdleLongOnFloor");
            sit = new PlayerSit(config, 102, "Sit");
            headUp = new PlayerHeadUp(config, 103, "HeadUp");
            walk = new PlayerMoveOnFloor(config, 104, "Walk");
            run = new PlayerMoveOnFloor(config, 105, "Run");

            m_pageRoot = BehaviorTree.Selector<EntityPlayer>(config, id, name, 5);
            m_isRun = BehaviorTree.IfElse<EntityPlayer>(config, -1, "PlayerIsRun");
            m_bIsRun = new PlayerIsRun(config, -1, "bIsRun");

            m_isRun.Alloc(0, m_bIsRun);
            m_isRun.Alloc(1, run);
            m_isRun.Alloc(2, walk);

            allocIndex = -1;
            m_pageRoot.Alloc(++allocIndex, m_isRun);
            m_pageRoot.Alloc(++allocIndex, sit);
            m_pageRoot.Alloc(++allocIndex, headUp);
            m_pageRoot.Alloc(++allocIndex, idleBasic);
            m_pageRoot.Alloc(++allocIndex, idleLong);
        }

        protected override InvokeResult p_Invoke()
        {
            // Debug here.
            walk.speedStat = config.instance.baseMoveSpeed;
            run.speedStat = config.instance.runSpeed;

            return m_pageRoot.Invoke();
        }
    }
}