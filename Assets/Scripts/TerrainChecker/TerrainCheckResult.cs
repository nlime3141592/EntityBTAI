using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class TerrainCheckResult
    {
        public bool bDetected;
        public bool bHit;
        public RaycastHit2D terrain;
    }
}