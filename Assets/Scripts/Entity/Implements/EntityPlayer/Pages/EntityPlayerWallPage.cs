using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerOnWallPage : PageNodeBT<EntityPlayer>
    {
        public readonly PlayerIdleOnWall idleWall;
        public readonly PlayerSlidingOnWall slidingWall;

        private SelectorNodeBT<EntityPlayer> m_pageRoot;

        public PlayerOnWallPage(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {
            idleWall = new PlayerIdleOnWall(config, 200, "IdleOnWall");
            slidingWall = new PlayerSlidingOnWall(config, 201, "SlidingOnWall");

            m_pageRoot = BehaviorTree.Selector<EntityPlayer>(config, id, name, 2);

            m_pageRoot.Alloc(0, idleWall);
            m_pageRoot.Alloc(1, slidingWall);
        }

        protected override InvokeResult p_Invoke()
        {
            // minSpeedStatY 기본값: -6.0f
            slidingWall.gravityStatY = config.instance.baseGravity;

            return m_pageRoot.Invoke();
        }
    }
}
/*
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
*/