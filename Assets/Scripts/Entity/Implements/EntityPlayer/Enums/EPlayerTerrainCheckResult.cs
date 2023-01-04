using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    public enum EPlayerTerrainCheckResult
    {
        Floor = 0, Ceil,
        WallLT, WallRT, WallLB, WallRB,
        LedgeFloorLT = 10, LedgeFloorRT, LedgeCeilLB, LedgeCeilRB,
        LedgeLT = 14, LedgeRT, LedgeLB, LedgeRB
    }
}