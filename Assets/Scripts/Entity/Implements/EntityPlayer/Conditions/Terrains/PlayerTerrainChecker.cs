using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerTerrainChecker : TerrainChecker<EntityPlayer>
    {
        public PlayerTerrainChecker(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}