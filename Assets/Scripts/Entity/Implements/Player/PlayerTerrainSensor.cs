using System;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public class PlayerTerrainSensor : TerrainSensor<Player>
    {
        // TODO: 지형 정보를 외부로 빼낼 수 있도록 public property를 제작해야 함.
        public readonly TerrainSenseData datFloor;
        public readonly TerrainSenseData datCeil;
        public readonly TerrainSenseData datWallFrontB;
        public readonly TerrainSenseData datWallFrontT;
        public readonly TerrainSenseData datWallBackB;
        public readonly TerrainSenseData datWallBackT;
        public readonly TerrainSenseData datCornerFrontB;
        public readonly TerrainSenseData datCornerFrontT;
        public readonly TerrainSenseData datCornerBackB;
        public readonly TerrainSenseData datCornerBackT;
        public readonly TerrainSenseData datCornerFrontVB;
        public readonly TerrainSenseData datCornerFrontVT;
        public readonly TerrainSenseData datCornerBackVB;
        public readonly TerrainSenseData datCornerBackVT;

        public bool bOnWallFront => datWallFrontB.bOnHit && datWallFrontT.bOnHit && datCornerFrontT.bOnDetected;
        public bool bOnWallBack => datWallBackB.bOnHit && datWallBackT.bOnHit;
        // public bool bOnLedge => datWallFrontT.bOnHit && !datCornerFrontT.bOnDetected && datCornerFrontVT.bOnDetected;
        public bool bOnLedge => false;

        public PlayerTerrainSensor()
        {
            datFloor = new TerrainSenseData();
            datCeil = new TerrainSenseData();
            datWallFrontB = new TerrainSenseData();
            datWallFrontT = new TerrainSenseData();
            datWallBackB = new TerrainSenseData();
            datWallBackT = new TerrainSenseData();
            datCornerFrontB = new TerrainSenseData();
            datCornerFrontT = new TerrainSenseData();
            datCornerBackB = new TerrainSenseData();
            datCornerBackT = new TerrainSenseData();
            datCornerFrontVB = new TerrainSenseData();
            datCornerFrontVT = new TerrainSenseData();
            datCornerBackVB = new TerrainSenseData();
            datCornerBackVT = new TerrainSenseData();

            float dLength = 0.5f;
            float hLength = 0.06f;

            datFloor.dLength = dLength;
            datCeil.dLength = dLength;
            datWallFrontB.dLength = dLength;
            datWallFrontT.dLength = dLength;
            datWallBackB.dLength = dLength;
            datWallBackT.dLength = dLength;
            datCornerFrontB.dLength = dLength;
            datCornerFrontT.dLength = dLength;
            datCornerBackB.dLength = dLength;
            datCornerBackT.dLength = dLength;

            datFloor.hLength = hLength;
            datCeil.hLength = hLength;
            datWallFrontB.hLength = hLength;
            datWallFrontT.hLength = hLength;
            datWallBackB.hLength = hLength;
            datWallBackT.hLength = hLength;
            datCornerFrontB.hLength = hLength;
            datCornerFrontT.hLength = hLength;
            datCornerBackB.hLength = hLength;
            datCornerBackT.hLength = hLength;
            datCornerFrontVB.hLength = -1;
            datCornerFrontVT.hLength = -1;
            datCornerBackVB.hLength = -1;
            datCornerBackVT.hLength = -1;

            // NOTE: 혹시나 flip Y 기능을 사용한다면,
            // (1) float ly = _player.lookDir.fy;
            // (2) 1 => ly; -1 => -ly;
            datFloor.direction.Set(0, -1);
            datCeil.direction.Set(0, 1);
            datCornerFrontVB.direction.Set(0, 1);
            datCornerFrontVT.direction.Set(0, -1);
            datCornerBackVB.direction.Set(0, 1);
            datCornerBackVT.direction.Set(0, -1);
        }

        protected override void SetOrigins(Player _player)
        {
            Bounds head = _player.hCol.head.bounds;
            Bounds feet = _player.hCol.feet.bounds;
            float lx = _player.lookDir.fx;

            datFloor.origin.Set(feet.center.x, feet.min.y);
            datCeil.origin.Set(head.center.x, head.max.y);
            datWallFrontB.origin.Set(feet.center.x + lx * feet.extents.x, feet.center.y);
            datWallFrontT.origin.Set(head.center.x + lx * head.extents.x, head.center.y);
            datWallBackB.origin.Set(feet.center.x - lx * feet.extents.x, feet.center.y);
            datWallBackT.origin.Set(head.center.x - lx * head.extents.x, head.center.y);
            datCornerFrontB.origin.Set(feet.center.x + lx * feet.extents.x, feet.min.y);
            datCornerFrontT.origin.Set(head.center.x + lx * head.extents.x, head.max.y);
            datCornerBackB.origin.Set(feet.center.x - lx * feet.extents.x, feet.min.y);
            datCornerBackT.origin.Set(head.center.x - lx * head.extents.x, head.max.y);

            // TODO;
            datCornerFrontVB.origin.Set(
                datCornerFrontB.origin.x + lx * 0.1f,
                datCornerFrontB.origin.y
                );
            datCornerFrontVT.origin.Set(
                datCornerFrontT.origin.x + lx * 0.1f,
                datCornerFrontT.origin.y
                );
            datCornerBackVB.origin.Set(
                datCornerBackB.origin.x - lx * 0.1f,
                datCornerBackB.origin.y
                );
            datCornerBackVT.origin.Set(
                datCornerBackT.origin.x - lx * 0.1f,
                datCornerBackT.origin.y
                );

            datWallFrontB.direction.Set(lx, 0);
            datWallFrontT.direction.Set(lx, 0);
            datWallBackB.direction.Set(-lx, 0);
            datWallBackT.direction.Set(-lx, 0);
            datCornerFrontB.direction.Set(lx, 0);
            datCornerFrontT.direction.Set(lx, 0);
            datCornerBackB.direction.Set(-lx, 0);
            datCornerBackT.direction.Set(-lx, 0);

            datCornerFrontVB.dLength = 0.9f * feet.extents.y;
            datCornerFrontVT.dLength = 0.9f * head.extents.y;
            datCornerBackVB.dLength = 0.9f * feet.extents.y;
            datCornerBackVT.dLength = 0.9f * head.extents.y;

            int tmpLayer = 1 << LayerMask.NameToLayer("Terrain");
            datFloor.targetLayer = tmpLayer;
            datCeil.targetLayer = tmpLayer;
            datWallFrontB.targetLayer = tmpLayer;
            datWallFrontT.targetLayer = tmpLayer;
            datWallBackB.targetLayer = tmpLayer;
            datWallBackT.targetLayer = tmpLayer;
            datCornerFrontB.targetLayer = tmpLayer;
            datCornerFrontT.targetLayer = tmpLayer;
            datCornerBackB.targetLayer = tmpLayer;
            datCornerBackT.targetLayer = tmpLayer;
            datCornerFrontVB.targetLayer = tmpLayer;
            datCornerFrontVT.targetLayer = tmpLayer;
            datCornerBackVB.targetLayer = tmpLayer;
            datCornerBackVT.targetLayer = tmpLayer;
        }

        protected override void DetectTerrains(Player player)
        {
            TerrainSensorBase.Sense(in datFloor);
            TerrainSensorBase.Sense(in datCeil);
            TerrainSensorBase.Sense(in datWallFrontB);
            TerrainSensorBase.Sense(in datWallFrontT);
            TerrainSensorBase.Sense(in datWallBackB);
            TerrainSensorBase.Sense(in datWallBackT);

            // TerrainSensorBase.Sense(in datCornerFrontB);
            // TerrainSensorBase.Sense(in datCornerFrontT);
            // TerrainSensorBase.Sense(in datCornerBackB);
            // TerrainSensorBase.Sense(in datCornerBackT);
            // TerrainSensorBase.Sense(in datCornerFrontVB);
            // TerrainSensorBase.Sense(in datCornerFrontVT);
            // TerrainSensorBase.Sense(in datCornerBackVB);
            // TerrainSensorBase.Sense(in datCornerBackVT);
        }

        protected override void SetDirectionVector(Player player)
        {
            float nx = 1;
            float ny = 0;

            if(datFloor.bOnDetected)
            {
                nx = datFloor.hitData.normal.x;
                ny = datFloor.hitData.normal.y;

                if(ny != 0)
                    ny = -nx / ny;

                nx = 1;
            }

            player.moveDir.Set(nx, ny);
        }
    }
}