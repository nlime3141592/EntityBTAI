namespace UnchordMetroidvania
{
    public class MonsterBaseAI<T> : PageNodeBT<T>
    where T : EntityMonster
    {
        public static int fps_perSecond = 50;

        private SelectorNodeBT<T> m_pageRoot;
            private IfElseNodeBT<T> m_isDead;
                private CheckDeadHealth<T> m_condi_checkDeadHealth;
                private DestroyEntityGameObject<T> m_onDead;
            private SequenceNodeBT<T> m_isLived;
                private CheckAggroRange<T> m_checkAggroRange;
                private IfElseNodeBT<T> m_checkAggroChange;
                    private CheckAggroChange<T> m_condi_checkAggroChange;
                    private WaitNodeBT<T> m_waitOnAggroChange;
                    private SuccessNodeBT<T> m_successor;
                private SequenceNodeBT<T> m_logicAI;
                    private NodeBT<T> m_executionNode;
                    private WaitNodeBT<T> m_waitOnActionChange;

        public MonsterBaseAI(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {
            m_pageRoot = BehaviorTree.Selector<T>(config, -1, "root", 2);
                m_isDead = BehaviorTree.IfOnly<T>(config, -1, "isDead");
                    m_condi_checkDeadHealth = new CheckDeadHealth<T>(config, -1, "checkDeadHealthCondi");
                    m_onDead = new DestroyEntityGameObject<T>(config, -1, "onDead");
                m_isLived = BehaviorTree.Sequence<T>(config, -1, "isLived", 3);
                    m_checkAggroRange = new CheckAggroRange<T>(config, -1, "checkAggroRange");
                    m_checkAggroChange = BehaviorTree.IfElse<T>(config, -1, "checkAggroChange");
                        m_condi_checkAggroChange = new CheckAggroChange<T>(config, -1, "checkAggroChangeCondi");
                        m_waitOnAggroChange = new WaitNodeBT<T>(config, -1, "waitOnAggroChange", m_SecondToFps(fps_perSecond, 1));
                        m_successor = new SuccessNodeBT<T>(config, -1, "success");
                    m_logicAI = BehaviorTree.Sequence<T>(config, -1, "logicAI", 2);

                        m_waitOnActionChange = new WaitNodeBT<T>(config, -1, "waitOnActionsChange", m_SecondToFps(fps_perSecond, 1));

            m_waitOnAggroChange.frameDifference = m_SecondToFps(fps_perSecond, 1);
            m_waitOnActionChange.frameDifference = m_SecondToFps(fps_perSecond, 1);

            m_pageRoot.Alloc(0, m_isDead);
            m_pageRoot.Alloc(1, m_isLived);
            m_isDead.Alloc(0, m_condi_checkDeadHealth);
            m_isDead.Alloc(1, m_onDead);
            m_isLived.Alloc(0, m_checkAggroRange);
            m_isLived.Alloc(1, m_checkAggroChange);
            m_isLived.Alloc(2, m_logicAI);
            m_checkAggroChange.Alloc(0, m_condi_checkAggroChange);
            m_checkAggroChange.Alloc(1, m_waitOnAggroChange);
            m_checkAggroChange.Alloc(2, m_successor);
            m_logicAI.Alloc(1, m_waitOnActionChange);
        }

        public void Set(NodeBT<T> executionNode)
        {
            m_executionNode = executionNode;
            m_logicAI.Alloc(0, executionNode);
        }

        protected override InvokeResult p_Invoke()
        {
            m_waitOnAggroChange.waitFrame = m_SecondToFps(fps_perSecond, config.instance.waitSecondOnChangeAggro);
            m_waitOnAggroChange.frameDifference = m_SecondToFps(fps_perSecond, config.instance.waitDiffSecondOnChangeAggro);

            m_waitOnActionChange.waitFrame = m_SecondToFps(fps_perSecond, config.instance.waitSecondOnChangeAction);
            m_waitOnActionChange.frameDifference = m_SecondToFps(fps_perSecond, config.instance.waitDiffSecondOnChangeAction);

            return m_pageRoot.Invoke();
        }

        private int m_SecondToFps(int fpsPerSecond, float second)
        {
            return (int)(fpsPerSecond * second);
        }
    }
}