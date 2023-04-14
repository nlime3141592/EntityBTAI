using System;
using UnityEngine;

namespace Unchord
{
    // [Serializable]
    public class TerrainSenseData
    {
        // TerrainSensor 호출 후 결정되는 변수
        [HideInInspector] public RaycastHit2D hitData;
        public bool bOnDetected;
        public bool bOnHit;

        // TerrainSensor 호출 전 결정해야 하는 변수
        [HideInInspector] public Vector2 origin;
        [HideInInspector] public Vector2 direction;
        [HideInInspector] public float dLength;
        [HideInInspector] public float hLength;
        [HideInInspector] public int targetLayer;
    }
}