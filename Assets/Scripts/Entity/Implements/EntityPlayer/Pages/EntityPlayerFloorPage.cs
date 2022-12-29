namespace UnchordMetroidvania
{
    public sealed class EntityPlayerFloorPage : EntityFloorPage
    {
        private IEntityPlayerConfig m_config;

        private IfElseReactiveNodeBT<IEntityPlayerConfig> root;
        private IfElseReactiveNodeBT<IEntityPlayerConfig> xQuery;
        private IfElseReactiveNodeBT<IEntityPlayerConfig> yQuery;
        private SequenceNodeBT<IEntityPlayerConfig> idleSeq;
        private IfElseNodeBT<IEntityPlayerConfig> moveQuery;
        private PostDelayNodeBT<IEntityPlayerConfig> idlePostDelay;

        public EntityPlayerFloorPage(IEntityPlayerConfig config,
            EntityMoveOnFloor<IEntityPlayerConfig> walkTask,
            EntityMoveOnFloor<IEntityPlayerConfig> runTask,
            EntityIdleOnFloor<IEntityPlayerConfig> idleBasicTask,
            EntityIdleOnFloor<IEntityPlayerConfig> idleLongTask,
            EntityIdleOnFloor<IEntityPlayerConfig> sitTask,
            EntityIdleOnFloor<IEntityPlayerConfig> headUpTask
        )
        {
            m_config = config;

            root = BehaviorTree.IfElseReactive<IEntityPlayerConfig>(m_config);
            xQuery = BehaviorTree.IfElseReactive<IEntityPlayerConfig>(m_config);
            yQuery = BehaviorTree.IfElseReactive<IEntityPlayerConfig>(m_config);
            idleSeq = BehaviorTree.Sequence<IEntityPlayerConfig>(m_config, 2);
            moveQuery = BehaviorTree.IfElse<IEntityPlayerConfig>(m_config);
            idlePostDelay = BehaviorTree.PostDelay<IEntityPlayerConfig>(m_config, idleBasicTask, m_config.maxIdleFrame);

            // TODO: root.Set(0, m_config.yInput == 0)
            root.Set(0, YInputCondition<IEntityPlayerConfig>.EqualsZero(m_config));
            root.Set(1, xQuery);
            root.Set(2, yQuery);

            // TODO: xQuery.Set(0, m_config.xInput == 0)
            xQuery.Set(0, XInputCondition<IEntityPlayerConfig>.EqualsZero(m_config));
            xQuery.Set(1, idleSeq);
            xQuery.Set(2, moveQuery);

            // TODO: yQuery.Set(0, m_config.yInput < 0)
            yQuery.Set(0, YInputCondition<IEntityPlayerConfig>.SmallerThanZero(m_config));
            SetSitTask(sitTask);
            SetHeadUpTask(headUpTask);

            idleSeq.Set(0, idlePostDelay);
            SetIdleLongTask(idleLongTask);

            // TODO: moveQuery.Set(0, m_config.isRun)
            moveQuery.Set(0, new EntityIsRun<IEntityPlayerConfig>(m_config));
            SetRunTask(runTask);
            SetWalkTask(walkTask);
        }

        public void SetWalkTask(EntityMoveOnFloor<IEntityPlayerConfig> task)
        {
            moveQuery.Set(2, task);
        }

        public void SetRunTask(EntityMoveOnFloor<IEntityPlayerConfig> task)
        {
            moveQuery.Set(1, task);
        }

        public void SetIdleBasicTask(EntityIdleOnFloor<IEntityPlayerConfig> task)
        {
            idlePostDelay.Set(task);
        }

        public void SetIdleLongTask(EntityIdleOnFloor<IEntityPlayerConfig> task)
        {
            idleSeq.Set(1, task);
        }

        public void SetSitTask(EntityIdleOnFloor<IEntityPlayerConfig> task)
        {
            yQuery.Set(1, task);
        }

        public void SetHeadUpTask(EntityIdleOnFloor<IEntityPlayerConfig> task)
        {
            yQuery.Set(2, task);
        }

        public override InvokeResult Invoke(long curFps)
        {
            idlePostDelay.SetFrame(m_config.maxIdleFrame);

            root.p_config.SetFps(curFps);
            return root.Invoke();
        }
    }
}