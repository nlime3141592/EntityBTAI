namespace UnchordMetroidvania
{
    public interface IEntityPlayerConfig
    : IEntityInputConfig, IEntityMovementConfig, IEntityRunConfig
    {
        IEntityPlayerConfig playerConfig { get; }

        ITerrainCheckerConfig floorConfig { get; set; }
        ITerrainCheckerConfig ceilConfig { get; set; }
        ITerrainCheckerConfig lbWallConfig { get; set; }
        ITerrainCheckerConfig rbWallConfig { get; set; }
        ITerrainCheckerConfig ltWallConfig { get; set; }
        ITerrainCheckerConfig rtWallConfig { get; set; }
        ITerrainCheckerConfig lbLedgeConfig { get; set; }
        ITerrainCheckerConfig rbLedgeConfig { get; set; }
        ITerrainCheckerConfig ltLedgeConfig { get; set; }
        ITerrainCheckerConfig rtLedgeConfig { get; set; }

        int maxIdleFrame { get; set; }
    }
}