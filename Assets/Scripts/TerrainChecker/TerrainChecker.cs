using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class TerrainChecker : TaskNodeBT<TerrainCheckResult>
    {
        public TerrainChecker(ConfigurationBT<TerrainCheckResult> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected int p_GetLayerMask()
        {
            return 1 << LayerMask.NameToLayer("Terrain");
        }
    }
}