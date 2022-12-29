namespace UnchordMetroidvania
{
    public sealed class EntityPlayerAirPage : EntityAirPage
    {
        private IEntityPlayerConfig m_config;

        private IfElseNodeBT<IEntityPlayerConfig> root;
        private EntityFreeFallOnAir<IEntityPlayerConfig> m_freeFallTask;
        private EntityGlidingOnAir<IEntityPlayerConfig> m_glidingTask;

        public EntityPlayerAirPage(
            IEntityPlayerConfig config,
            EntityFreeFallOnAir<IEntityPlayerConfig> freeFallTask,
            EntityGlidingOnAir<IEntityPlayerConfig> glidingTask
        )
        {
            m_config = config;

            root = BehaviorTree.IfElse<IEntityPlayerConfig>(m_config);

            m_freeFallTask = freeFallTask;
            m_glidingTask = glidingTask;

            root.Set(0, YInputCondition<IEntityPlayerConfig>.BiggerThanZero(m_config));
            root.Set(1, m_glidingTask);
            root.Set(2, m_freeFallTask);
        }

        public override InvokeResult Invoke(long curFps)
        {
            root.p_config.SetFps(curFps);
            return root.Invoke();
        }
    }
}