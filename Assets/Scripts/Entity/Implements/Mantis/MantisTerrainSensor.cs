using System;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public class MantisTerrainSensor : TerrainSensor<Mantis>
    {
        public readonly TerrainSenseData datFloorBack;
        public readonly TerrainSenseData datFloorFront;
        public readonly TerrainSenseData datCeilBack;
        public readonly TerrainSenseData datCeilFront;
        public readonly TerrainSenseData datWallFrontB;
        public readonly TerrainSenseData datWallFrontT;
        public readonly TerrainSenseData datWallBackB;
        public readonly TerrainSenseData datWallBackT;

        public bool bOnWallFront => datWallFrontB.bOnDetected && datWallFrontT.bOnDetected;
        public bool bOnWallBack => datWallBackB.bOnDetected && datWallBackT.bOnDetected;

        public MantisTerrainSensor()
        {
            datFloorBack = new TerrainSenseData();
            datFloorFront = new TerrainSenseData();
            datCeilBack = new TerrainSenseData();
            datCeilFront = new TerrainSenseData();
            datWallFrontB = new TerrainSenseData();
            datWallFrontT = new TerrainSenseData();
            datWallBackB = new TerrainSenseData();
            datWallBackT = new TerrainSenseData();

            float dLength = 0.5f;
            float hLength = 0.06f;

            datFloorBack.dLength = dLength;
            datFloorFront.dLength = dLength;
            datCeilBack.dLength = dLength;
            datCeilFront.dLength = dLength;
            datWallFrontB.dLength = dLength;
            datWallFrontT.dLength = dLength;
            datWallBackB.dLength = dLength;
            datWallBackT.dLength = dLength;

            datFloorBack.hLength = hLength;
            datFloorFront.hLength = hLength;
            datCeilBack.hLength = hLength;
            datCeilFront.hLength = hLength;
            datWallFrontB.hLength = hLength;
            datWallFrontT.hLength = hLength;
            datWallBackB.hLength = hLength;
            datWallBackT.hLength = hLength;

            datFloorBack.direction.Set(0, -1);
            datFloorFront.direction.Set(0, -1);
            datCeilBack.direction.Set(0, 1);
            datCeilFront.direction.Set(0, 1);
        }

        protected override void SetOrigins(Mantis _instance)
        {
            // Bounds box = _instance.terrainCollider.bounds;
            Bounds box = (_instance.volumeCollisions[0] as BoxCollider2D).bounds;

            float minY = box.min.y;
            float maxY = box.max.y;
            float cx = box.center.x;
            float dx = box.extents.x;
            float y10 = minY + (maxY - minY) * 0.1f;
            float y90 = maxY - (maxY - minY) * 0.1f;
            float lx = _instance.lookDir.fx;

            datFloorBack.origin.Set(cx - lx * dx, minY);
            datFloorFront.origin.Set(cx + lx * dx, minY);
            datCeilBack.origin.Set(cx - lx * dx, maxY);
            datCeilFront.origin.Set(cx + lx * dx, maxY);
            datWallFrontB.origin.Set(cx + lx * dx, y10);
            datWallFrontT.origin.Set(cx + lx * dx, y90);
            datWallBackB.origin.Set(cx - lx * dx, y10);
            datWallBackT.origin.Set(cx - lx * dx, y90);

            datWallFrontB.direction.Set(lx, 0);
            datWallFrontT.direction.Set(lx, 0);
            datWallBackB.direction.Set(-lx, 0);
            datWallBackT.direction.Set(-lx, 0);

            int tmpLayer = 1 << LayerMask.NameToLayer("Terrain");
            datFloorBack.targetLayer = tmpLayer;
            datFloorFront.targetLayer = tmpLayer;
            datCeilBack.targetLayer = tmpLayer;
            datCeilFront.targetLayer = tmpLayer;
            datWallFrontB.targetLayer = tmpLayer;
            datWallFrontT.targetLayer = tmpLayer;
            datWallBackB.targetLayer = tmpLayer;
            datWallBackT.targetLayer = tmpLayer;
        }

        protected override void DetectTerrains(Mantis _instance)
        {
            TerrainSensorBase.Sense(in datFloorBack);
            TerrainSensorBase.Sense(in datFloorFront);
            TerrainSensorBase.Sense(in datCeilBack);
            TerrainSensorBase.Sense(in datCeilFront);
            TerrainSensorBase.Sense(in datWallFrontB);
            TerrainSensorBase.Sense(in datWallFrontT);
            TerrainSensorBase.Sense(in datWallBackB);
            TerrainSensorBase.Sense(in datWallBackT);
        }

        protected override void SetDirectionVector(Mantis _instance)
        {
            float nx = 1;
            float ny = 0;

            if(datFloorFront.bOnDetected)
            {
                nx = datFloorFront.hitData.normal.x;
                ny = datFloorBack.hitData.normal.y;

                if(ny != 0)
                    ny = -nx / ny;

                nx = 1;
            }

            _instance.moveDir.Set(nx, ny);
        }
    }
}