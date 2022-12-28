namespace UnchordMetroidvania
{
    public abstract class TerrainChecker : TaskNodeBT<ITerrainCheckerConfig>
    {
        protected TerrainChecker(ITerrainCheckerConfig config, int id, string name)
        : base(config, id, name)
        {
            
        }
    }
}