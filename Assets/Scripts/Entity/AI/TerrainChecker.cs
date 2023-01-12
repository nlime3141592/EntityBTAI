using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class TerrainChecker<T> : TaskNodeBT<T>
    where T : EntityBase
    {
        public TerrainChecker(T instance)
        : base(instance)
        {

        }

        protected int p_GetLayerMask()
        {
            return 1 << LayerMask.NameToLayer("Terrain");
        }
    }
}