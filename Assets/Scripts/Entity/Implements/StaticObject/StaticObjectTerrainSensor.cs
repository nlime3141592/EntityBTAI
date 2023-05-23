using System;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public class StaticObjectTerrainSensor : TerrainSensor<StaticObject>
    {
        public readonly TerrainSenseData datFloorL;
        public readonly TerrainSenseData datFloorR;

        public StaticObjectTerrainSensor()
        {
            datFloorL = new TerrainSenseData();
            datFloorR = new TerrainSenseData();

            float dLength = 0.5f;
            float hLength = 0.06f;

            datFloorL.direction.Set(0, -1);
            datFloorR.direction.Set(0, -1);

            datFloorL.dLength = dLength;
            datFloorR.dLength = dLength;

            datFloorL.hLength = hLength;
            datFloorR.hLength = hLength;
        }

        protected override void SetOrigins(StaticObject _instance)
        {
            Bounds box = (_instance.volumeCollisions[0] as BoxCollider2D).bounds;

            datFloorL.origin.Set(box.min.x, box.min.y);
            datFloorR.origin.Set(box.max.x, box.min.y);
        }

        protected override void DetectTerrains(StaticObject _instance)
        {
            TerrainSensorBase.Sense(in datFloorL);
            TerrainSensorBase.Sense(in datFloorR);
        }

        protected override void SetDirectionVector(StaticObject _instance)
        {
            float nx = 1;
            float ny = 0;

            if(datFloorL.bOnDetected)
            {
                nx = datFloorL.hitData.normal.x;
                ny = datFloorL.hitData.normal.y;

                if(ny != 0)
                    ny = -nx / ny;

                nx = 1;
            }
            if(datFloorR.bOnDetected)
            {
                nx = datFloorR.hitData.normal.x;
                ny = datFloorR.hitData.normal.y;

                if(ny != 0)
                    ny = -nx / ny;

                nx = 1;
            }
            
            _instance.moveDir.Set(nx, ny);
        }
    }
}