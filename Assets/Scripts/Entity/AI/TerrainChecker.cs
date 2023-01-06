using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class TerrainChecker<T> : TaskNodeBT<T>
    where T : EntityBase
    {
        public TerrainChecker(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected int p_GetLayerMask()
        {
            return 1 << LayerMask.NameToLayer("Terrain");
        }
    }
}