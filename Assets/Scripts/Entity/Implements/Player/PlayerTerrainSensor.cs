using System;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public class PlayerTerrainSensor : TerrainSensor<Player>
    {
        // TODO: 지형 정보를 외부로 빼낼 수 있도록 public property를 제작해야 함.



        public bool bOnDetectFloor => m_datFloor.bOnDetected;
        public bool bOnFloor => m_datFloor.bOnHit;
        private TerrainSenseData m_datFloor;

        public bool bOnDetectCeil => m_datCeil.bOnDetected;
        public bool bOnCeil => m_datCeil.bOnHit;
        private TerrainSenseData m_datCeil;

        public bool bOnDetectWallFrontB => m_datWallFrontB.bOnDetected;
        public bool bOnWallFrontB => m_datWallFrontB.bOnHit;
        private TerrainSenseData m_datWallFrontB;

        public bool bOnDetectWallFrontT => m_datWallFrontT.bOnDetected;
        public bool bOnWallFrontT => m_datWallFrontT.bOnHit;
        private TerrainSenseData m_datWallFrontT;

        public bool bOnDetectWallBackB => m_datWallBackB.bOnDetected;
        public bool bOnWallBackB => m_datWallBackB.bOnHit;
        private TerrainSenseData m_datWallBackB;

        public bool bOnDetectWallBackT => m_datWallBackT.bOnDetected;
        public bool bOnWallBackT => m_datWallBackT.bOnHit;
        private TerrainSenseData m_datWallBackT;

        public bool bOnDetectCornerFrontB => m_datCornerFrontB.bOnDetected;
        public bool bOnCornerFrontB => m_datCornerFrontB.bOnHit;
        private TerrainSenseData m_datCornerFrontB;

        public bool bOnDetectCornerFrontT => m_datCornerFrontT.bOnDetected;
        public bool bOnCornerFrontT => m_datCornerFrontT.bOnHit;
        private TerrainSenseData m_datCornerFrontT;

        public bool bOnDetectCornerBackB => m_datCornerBackB.bOnDetected;
        public bool bOnCornerBackB => m_datCornerBackB.bOnHit;
        private TerrainSenseData m_datCornerBackB;

        public bool bOnDetectCornerBackT => m_datCornerBackT.bOnDetected;
        public bool bOnCornerBackT => m_datCornerBackT.bOnHit;
        private TerrainSenseData m_datCornerBackT;

        public bool bOnDetectCornerFrontVB => m_datCornerFrontVB.bOnDetected;
        public bool bOnCornerFrontVB => m_datCornerFrontVB.bOnHit;
        private TerrainSenseData m_datCornerFrontVB;

        public bool bOnDetectCornerFrontVT => m_datCornerFrontVT.bOnDetected;
        public bool bOnCornerFrontVT => m_datCornerFrontVT.bOnHit;
        private TerrainSenseData m_datCornerFrontVT;

        public bool bOnDetectCornerBackVB => m_datCornerBackVB.bOnDetected;
        public bool bOnCornerBackVB => m_datCornerBackVB.bOnHit;
        private TerrainSenseData m_datCornerBackVB;

        public bool bOnDetectCornerBackVT => m_datCornerBackVT.bOnDetected;
        public bool bOnCornerBackVT => m_datCornerBackVT.bOnHit;
        private TerrainSenseData m_datCornerBackVT;

        public bool bOnWallFront => bOnWallFrontB && bOnWallFrontT && bOnCornerFrontT;
        public bool bOnWallBack => bOnWallBackB && bOnWallBackT;
        public bool bOnLedge => bOnWallFrontT && !bOnCornerFrontT;

        public PlayerTerrainSensor()
        {
            m_datFloor = new TerrainSenseData();
            m_datCeil = new TerrainSenseData();
            m_datWallFrontB = new TerrainSenseData();
            m_datWallFrontT = new TerrainSenseData();
            m_datWallBackB = new TerrainSenseData();
            m_datWallBackT = new TerrainSenseData();
            m_datCornerFrontB = new TerrainSenseData();
            m_datCornerFrontT = new TerrainSenseData();
            m_datCornerBackB = new TerrainSenseData();
            m_datCornerBackT = new TerrainSenseData();
            m_datCornerFrontVB = new TerrainSenseData();
            m_datCornerFrontVT = new TerrainSenseData();
            m_datCornerBackVB = new TerrainSenseData();
            m_datCornerBackVT = new TerrainSenseData();

            float dLength = 0.5f;
            float hLength = 0.06f;

            m_datFloor.dLength = dLength;
            m_datCeil.dLength = dLength;
            m_datWallFrontB.dLength = dLength;
            m_datWallFrontT.dLength = dLength;
            m_datWallBackB.dLength = dLength;
            m_datWallBackT.dLength = dLength;
            m_datCornerFrontB.dLength = dLength;
            m_datCornerFrontT.dLength = dLength;
            m_datCornerBackB.dLength = dLength;
            m_datCornerBackT.dLength = dLength;

            m_datFloor.hLength = hLength;
            m_datCeil.hLength = hLength;
            m_datWallFrontB.hLength = hLength;
            m_datWallFrontT.hLength = hLength;
            m_datWallBackB.hLength = hLength;
            m_datWallBackT.hLength = hLength;
            m_datCornerFrontB.hLength = hLength;
            m_datCornerFrontT.hLength = hLength;
            m_datCornerBackB.hLength = hLength;
            m_datCornerBackT.hLength = hLength;

            m_datFloor.direction.Set(0, -1);
            m_datCeil.direction.Set(0, 1);

            m_datCornerFrontVB.hLength = -1;
            m_datCornerFrontVT.hLength = -1;
            m_datCornerBackVB.hLength = -1;
            m_datCornerBackVT.hLength = -1;

            int tmpLayer = 1 << LayerMask.NameToLayer("Terrain");
            m_datFloor.targetLayer = tmpLayer;
            m_datCeil.targetLayer = tmpLayer;
            m_datWallFrontB.targetLayer = tmpLayer;
            m_datWallFrontT.targetLayer = tmpLayer;
            m_datWallBackB.targetLayer = tmpLayer;
            m_datWallBackT.targetLayer = tmpLayer;
            m_datCornerFrontB.targetLayer = tmpLayer;
            m_datCornerFrontT.targetLayer = tmpLayer;
            m_datCornerBackB.targetLayer = tmpLayer;
            m_datCornerBackT.targetLayer = tmpLayer;
            m_datCornerFrontVB.targetLayer = tmpLayer;
            m_datCornerFrontVT.targetLayer = tmpLayer;
            m_datCornerBackVB.targetLayer = tmpLayer;
            m_datCornerBackVT.targetLayer = tmpLayer;
        }

        protected override void SetOrigins(Player _player)
        {
            Bounds head = _player.hCol.head.bounds;
            Bounds feet = _player.hCol.feet.bounds;
            float lx = _player.lookDir.fx;

            m_datFloor.origin.Set(feet.center.x, feet.min.y);
            m_datCeil.origin.Set(head.center.x, head.max.y);
            m_datWallFrontB.origin.Set(feet.center.x + lx * feet.extents.x, feet.center.y);
            m_datWallFrontT.origin.Set(head.center.x + lx * head.extents.x, head.center.y);
            m_datWallBackB.origin.Set(feet.center.x - lx * feet.extents.x, feet.center.y);
            m_datWallBackT.origin.Set(head.center.x - lx * head.extents.x, head.center.y);
            m_datCornerFrontB.origin.Set(feet.center.x + lx * feet.extents.x, feet.min.y);
            m_datCornerFrontT.origin.Set(head.center.x + lx * head.extents.x, head.max.y);
            m_datCornerBackB.origin.Set(feet.center.x - lx * feet.extents.x, feet.min.y);
            m_datCornerBackT.origin.Set(head.center.x - lx * head.extents.x, head.max.y);

            // TODO;
            // m_datCornerFrontVB.origin.Set(0, 1);
            // m_datCornerFrontVT.origin.Set(0, -1);
            // m_datCornerBackVB.origin.Set(0, 1);
            // m_datCornerBackVT.origin.Set(0, -1);

            m_datWallFrontB.direction.Set(lx, 0);
            m_datWallFrontT.direction.Set(lx, 0);
            m_datWallBackB.direction.Set(-lx, 0);
            m_datWallBackT.direction.Set(-lx, 0);
            m_datCornerFrontB.direction.Set(lx, 0);
            m_datCornerFrontT.direction.Set(lx, 0);
            m_datCornerBackB.direction.Set(-lx, 0);
            m_datCornerBackT.direction.Set(-lx, 0);

            // NOTE: 혹시나 flip Y 기능을 사용한다면,
            // (1) float ly = _player.lookDir.fy;
            // (2) 1 => ly; -1 => -ly;
            m_datCornerFrontVB.direction.Set(0, 1);
            m_datCornerFrontVT.direction.Set(0, -1);
            m_datCornerBackVB.direction.Set(0, 1);
            m_datCornerBackVT.direction.Set(0, -1);

            m_datCornerFrontVB.dLength = feet.max.y - feet.min.y;
            m_datCornerFrontVT.dLength = head.max.y - head.min.y;
            m_datCornerBackVB.dLength = feet.max.y - feet.min.y;
            m_datCornerBackVT.dLength = head.max.y - head.min.y;
        }

        protected override void DetectTerrains(Player player)
        {
            float lx = player.lookDir.fx;

            TerrainSensorBase.Sense(in m_datFloor);
            TerrainSensorBase.Sense(in m_datCeil);
            TerrainSensorBase.Sense(in m_datWallFrontB);
            TerrainSensorBase.Sense(in m_datWallFrontT);
            TerrainSensorBase.Sense(in m_datWallBackB);
            TerrainSensorBase.Sense(in m_datWallBackT);
            TerrainSensorBase.Sense(in m_datCornerFrontB);
            TerrainSensorBase.Sense(in m_datCornerFrontT);
            TerrainSensorBase.Sense(in m_datCornerBackB);
            TerrainSensorBase.Sense(in m_datCornerBackT);
            TerrainSensorBase.Sense(in m_datCornerFrontVB);
            TerrainSensorBase.Sense(in m_datCornerFrontVT);
            TerrainSensorBase.Sense(in m_datCornerBackVB);
            TerrainSensorBase.Sense(in m_datCornerBackVT);
        }

        protected override void SetDirectionVector(Player player)
        {
            float nx = 1;
            float ny = 0;

            if(m_datFloor.bOnDetected)
            {
                nx = m_datFloor.hitData.normal.x;
                ny = m_datFloor.hitData.normal.y;

                if(ny != 0)
                    ny = -nx / ny;

                nx = 1;
            }

            player.moveDir.Set(nx, ny);
        }
    }
}