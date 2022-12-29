using UnityEngine;
namespace UnchordMetroidvania
{
    public sealed class EntityPlayerWallPage : EntityWallPage
    {
        private IEntityPlayerConfig m_config;

        private IfElseNodeBT<IEntityPlayerConfig> root;
        private EntityIdleOnWall<IEntityPlayerConfig> m_idleWall;
        private EntitySlidingOnWall<IEntityPlayerConfig> m_slidingWall;

        public EntityPlayerWallPage(
            IEntityPlayerConfig config,
            EntityIdleOnWall<IEntityPlayerConfig> idleWall,
            EntitySlidingOnWall<IEntityPlayerConfig> slidingWall
        )
        {
            m_config = config;

            root = BehaviorTree.IfElse<IEntityPlayerConfig>(m_config);

            m_idleWall = idleWall;
            m_slidingWall = slidingWall;

            root.Set(0, XInputCondition<IEntityPlayerConfig>.EqualsZero(m_config));
            root.Set(1, m_slidingWall);
            root.Set(2, m_idleWall);
        }

        public override InvokeResult Invoke(long curFps)
        {
            root.p_config.SetFps(curFps);
            return root.Invoke();
        }
    }
}