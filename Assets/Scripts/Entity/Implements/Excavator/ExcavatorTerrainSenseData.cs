using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class ExcavatorTerrainSenseData : TerrainSenseData<Excavator>
    {
        [HideInInspector] public bool bOnFloor;
        [HideInInspector] public bool bOnCeil;
        [HideInInspector] public bool bOnWallFront;
        [HideInInspector] public bool bOnWallBack;

        public override void UpdateOrigins(Excavator instane)
        {
            
        }

        public override void UpdateData(Excavator instance)
        {
            
        }

        public override void UpdateMoveDir(Excavator instance)
        {
            
        }
    }
}