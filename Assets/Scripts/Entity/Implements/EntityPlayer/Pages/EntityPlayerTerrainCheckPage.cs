namespace UnchordMetroidvania
{
    public class EntityPlayerTerrainCheckPage : EntityTerrainCheckPage
    {
        private ITerrainCheckerConfig m_config;

        private ParallelNodeBT<ITerrainCheckerConfig> root;

        public EntityPlayerTerrainCheckPage(
            ITerrainCheckerConfig config,
            FloorChecker fChecker, CeilChecker cChecker,
            WallChecker lbwChecker, WallChecker rbwChecker,
            WallChecker ltwChecker, WallChecker rtwChecker,
            LedgeChecker lblChecker, LedgeChecker rblChecker,
            LedgeChecker ltlChecker, LedgeChecker rtlChecker
            )
        {
            m_config = config;

            root = BehaviorTree.Parallel<ITerrainCheckerConfig>(m_config, 10);

            root.Set(0, fChecker);
            root.Set(1, cChecker);
            root.Set(2, lbwChecker);
            root.Set(3, rbwChecker);
            root.Set(4, ltwChecker);
            root.Set(5, rtwChecker);
            root.Set(6, lblChecker);
            root.Set(7, rblChecker);
            root.Set(8, ltlChecker);
            root.Set(9, rtlChecker);
        }

        public override InvokeResult Invoke(long curFps)
        {
            return root.Invoke();
        }
    }
}