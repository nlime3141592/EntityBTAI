namespace UnchordMetroidvania
{
    public class PlayerFloorChecker : PlayerTerrainChecker
    {
        public PlayerFloorChecker(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            /*
            EntityPlayer player = config.instance;
            Transform[] transforms = player.origins;
            TerrainCheckResult[] results = player.terrainCheckResults;

            TerrainCheckResult result = results[0];

            result.terrain = Physics2D.Raycast(transforms[0], Vector2.down, 0.5f, p_GetLayerMask());
            result.bDetected = result.terrain;
            result.bHit = result.terrain.hit <= 0.04f;
*/
            return InvokeResult.Success;
        }
    }
}