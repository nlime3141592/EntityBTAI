using System;
using UnityEngine;

namespace Unchord
{
    public class ExcavatorWaveTerrainSensor : TerrainSensor<ExcavatorWave>
    {
        public readonly TerrainSenseData datFloor;
        public readonly TerrainSenseData datCeil;
        public readonly TerrainSenseData datWallLeft;
        public readonly TerrainSenseData datWallRight;

        public ExcavatorWaveTerrainSensor()
        {
            datFloor = new TerrainSenseData();
            datCeil = new TerrainSenseData();
            datWallLeft = new TerrainSenseData();
            datWallRight = new TerrainSenseData();

            float dLength = 0.1f;
            float hLength = 0.06f;

            datFloor.dLength = dLength;
            datCeil.dLength = dLength;
            datWallLeft.dLength = dLength;
            datWallRight.dLength = dLength;

            datFloor.hLength = hLength;
            datCeil.hLength = hLength;
            datWallLeft.hLength = hLength;
            datWallRight.hLength = hLength;

            int layerMask = 1 << LayerMask.NameToLayer("Terrain");
            datFloor.targetLayer = layerMask;
            datCeil.targetLayer = layerMask;
            datWallLeft.targetLayer = layerMask;
            datWallRight.targetLayer = layerMask;
        }

        protected override void SetOrigins(ExcavatorWave _wave)
        {

        }

        protected override void DetectTerrains(ExcavatorWave _wave)
        {
            TerrainSensorBase.Sense(in datFloor);
            TerrainSensorBase.Sense(in datCeil);
            TerrainSensorBase.Sense(in datWallLeft);
            TerrainSensorBase.Sense(in datWallRight);
        }
    }
}