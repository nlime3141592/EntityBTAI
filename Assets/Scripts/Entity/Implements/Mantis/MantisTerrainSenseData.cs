using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class MantisTerrainSenseData : TerrainSenseData<Mantis>
    {
        public Transform originFloorL;
        public Transform originFloorR;
        public Transform originCeilL;
        public Transform originCeilR;
        public Transform originWallLT;
        public Transform originWallRT;
        public Transform originWallLB;
        public Transform originWallRB;

        [HideInInspector] public bool bOnFloor;
        [HideInInspector] public bool bOnCeil;
        [HideInInspector] public bool bOnWallFront;
        [HideInInspector] public bool bOnWallBack;

        private bool m_tmp_bOnFloorL;
        private bool m_tmp_bOnFloorR;
        private bool m_tmp_bOnCeilL;
        private bool m_tmp_bOnCeilR;
        private bool m_tmp_bOnWallLT;
        private bool m_tmp_bOnWallRT;
        private bool m_tmp_bOnWallLB;
        private bool m_tmp_bOnWallRB;

        public override void UpdateOrigins(Mantis mantis)
        {
            Bounds box = mantis.terrainCollider.bounds;
            float minX = box.min.x;
            float minY = box.min.y;
            float maxX = box.max.x;
            float maxY = box.max.y;
            float y10 = minY + (maxY - minY) * 0.1f;
            float y90 = maxY - (maxY - minY) * 0.1f;

            originFloorL.position = new Vector2(minX, minY);
            originFloorR.position = new Vector2(maxX, minY);
            originCeilL.position = new Vector2(minX, maxY);
            originCeilR.position = new Vector2(maxX, maxY);
            originWallLT.position = new Vector2(minX, y90);
            originWallRT.position = new Vector2(maxX, y90);
            originWallLB.position = new Vector2(minX, y10);
            originWallRB.position = new Vector2(maxX, y10);
        }

        public override void UpdateData(Mantis mantis)
        {
            MantisData data = mantis.data;
            float lx = mantis.lookDir.x;

            m_tmp_bOnFloorL = TerrainSensor.CheckFloor(originFloorL.position, data.hitLength);
            m_tmp_bOnFloorR = TerrainSensor.CheckFloor(originFloorR.position, data.hitLength);
            m_tmp_bOnCeilL = TerrainSensor.CheckCeil(originCeilL.position, data.hitLength);
            m_tmp_bOnCeilR = TerrainSensor.CheckCeil(originCeilR.position, data.hitLength);

            if(lx > 0)
            {
                m_tmp_bOnWallLT = TerrainSensor.CheckWallBack(originWallLT.position, data.wallDetectLength, lx);
                m_tmp_bOnWallLB = TerrainSensor.CheckWallBack(originWallLB.position, data.wallDetectLength, lx);
                m_tmp_bOnWallRT = TerrainSensor.CheckWallFront(originWallRT.position, data.wallDetectLength, lx);
                m_tmp_bOnWallRB = TerrainSensor.CheckWallFront(originWallRB.position, data.wallDetectLength, lx);

                bOnWallFront = m_tmp_bOnWallRT || m_tmp_bOnWallRB;
                bOnWallBack = m_tmp_bOnWallLT || m_tmp_bOnWallRT;
            }
            else
            {
                m_tmp_bOnWallLT = TerrainSensor.CheckWallFront(originWallLT.position, data.wallDetectLength, lx);
                m_tmp_bOnWallLB = TerrainSensor.CheckWallFront(originWallLB.position, data.wallDetectLength, lx);
                m_tmp_bOnWallRT = TerrainSensor.CheckWallBack(originWallRT.position, data.wallDetectLength, lx);
                m_tmp_bOnWallRB = TerrainSensor.CheckWallBack(originWallRB.position, data.wallDetectLength, lx);

                bOnWallFront = m_tmp_bOnWallLT || m_tmp_bOnWallRT;
                bOnWallBack = m_tmp_bOnWallRT || m_tmp_bOnWallRB;
            }

            bOnFloor = m_tmp_bOnFloorL || m_tmp_bOnFloorR;
            bOnCeil = m_tmp_bOnCeilL || m_tmp_bOnCeilR;
        }

        public override void UpdateMoveDir(Mantis mantis)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");
            RaycastHit2D terrainL = Physics2D.Raycast(originFloorL.position, Vector2.down, 0.5f, layerMask);
            RaycastHit2D terrainR = Physics2D.Raycast(originFloorR.position, Vector2.down, 0.5f, layerMask);
            RaycastHit2D terrain;

            mantis.moveDir.x = 1.0f;

            if(terrainL && terrainR)
                if(mantis.lookDir.x < 0)
                    terrain = terrainL;
                else
                    terrain = terrainR;
            else if(terrainL)
                terrain = terrainL;
            else if(terrainR)
                terrain = terrainR;
            else
                terrain = default(RaycastHit2D);

            if(terrain.normal.y == 0)
                mantis.moveDir.y = 0;
            else
                mantis.moveDir.y = -terrain.normal.x / terrain.normal.y;
        }
    }
}