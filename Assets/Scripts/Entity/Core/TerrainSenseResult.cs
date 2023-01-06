using UnityEngine;

namespace UnchordMetroidvania
{
    public struct TerrainSenseResult
    {
        public bool bDetected;
        public bool bHit;
        public RaycastHit2D terrain;
    }
}