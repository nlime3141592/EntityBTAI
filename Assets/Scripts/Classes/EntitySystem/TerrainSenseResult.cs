using UnityEngine;

namespace Unchord
{
    public struct TerrainSenseResult
    {
        public bool bDetected;
        public bool bHit;
        public RaycastHit2D terrain;
    }
}