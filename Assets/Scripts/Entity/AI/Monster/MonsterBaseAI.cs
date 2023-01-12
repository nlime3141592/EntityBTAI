namespace UnchordMetroidvania
{
    public class MonsterBaseAI<T> : PageNodeBT<T>
    where T : EntityMonster
    {
        public static int fps_perSecond = 50;

        private SelectorNodeBT<T> m_pageRoot;
            private IfOnlyNodeBT<T> m_isDead;
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

        public MonsterBaseAI(T instance)
        : base(instance)
        {
            m_pageRoot = BehaviorTree.Selector<T>(instance, 2);
                m_isDead = BehaviorTree.IfOnly<T>(instance);
                    m_condi_checkDeadHealth = new CheckDeadHealth<T>(instance);
                    m_onDead = new DestroyEntityGameObject<T>(instance);
                m_isLived = BehaviorTree.Sequence<T>(instance, 3);
                    m_checkAggroRange = new CheckAggroRange<T>(instance);
                    m_checkAggroChange = BehaviorTree.IfElse<T>(instance);
                        m_condi_checkAggroChange = new CheckAggroChange<T>(instance);
                        m_waitOnAggroChange = new WaitNodeBT<T>(instance);
                        m_successor = new SuccessNodeBT<T>(instance);
                    m_logicAI = BehaviorTree.Sequence<T>(instance, 2);

                        m_waitOnActionChange = new WaitNodeBT<T>(instance);


            m_waitOnAggroChange.waitCount = m_SecondToFps(fps_perSecond, instance.waitSecondOnChangeAggro);
            m_waitOnAggroChange.cntDeviation = m_SecondToFps(fps_perSecond, instance.waitDiffSecondOnChangeAggro);

            m_waitOnActionChange.waitCount = m_SecondToFps(fps_perSecond, instance.waitSecondOnChangeAction);
            m_waitOnActionChange.cntDeviation = m_SecondToFps(fps_perSecond, instance.waitDiffSecondOnChangeAction);

            m_pageRoot[0] = m_isDead;
            m_pageRoot[1] = m_isLived;
            m_isDead[0] = m_condi_checkDeadHealth;
            m_isDead[1] = m_onDead;
            m_isLived[0] = m_checkAggroRange;
            m_isLived[1] = m_checkAggroChange;
            m_isLived[2] = m_logicAI;
            m_checkAggroChange[0] = m_condi_checkAggroChange;
            m_checkAggroChange[1] = m_waitOnAggroChange;
            m_checkAggroChange[2] = m_successor;
            m_logicAI[1] = m_waitOnActionChange;
        }

        public void Set(NodeBT<T> executionNode)
        {
            m_executionNode = executionNode;
            m_logicAI[0] = executionNode;
        }

        protected override InvokeResult p_Invoke()
        {
            m_waitOnAggroChange.waitCount = m_SecondToFps(fps_perSecond, instance.waitSecondOnChangeAggro);
            m_waitOnAggroChange.cntDeviation = m_SecondToFps(fps_perSecond, instance.waitDiffSecondOnChangeAggro);

            m_waitOnActionChange.waitCount = m_SecondToFps(fps_perSecond, instance.waitSecondOnChangeAction);
            m_waitOnActionChange.cntDeviation = m_SecondToFps(fps_perSecond, instance.waitDiffSecondOnChangeAction);

            return m_pageRoot.Invoke();
        }

        private int m_SecondToFps(int fpsPerSecond, float second)
        {
            return (int)(fpsPerSecond * second);
        }
    }
}