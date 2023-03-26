using System;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public class PlayerTerrainSenseData : TerrainSenseData<Player>
    {
        // NOTE: Should allocate on hierarchy.
        public Transform originFloor;
        public Transform originCeil;
        public Transform originWallLT;
        public Transform originWallRT;
        public Transform originWallLB;
        public Transform originWallRB;
        public Transform originLedgeLT;
        public Transform originLedgeRT;
        public Transform originLedgeLB;
        public Transform originLedgeRB;

        [HideInInspector] public bool bOnDetectFloor;
        [HideInInspector] public bool bOnFloor;
        [HideInInspector] public bool bOnCeil;
        [HideInInspector] public bool bOnWallFrontT;
        [HideInInspector] public bool bOnWallFrontB;
        [HideInInspector] public bool bOnWallFront;
        [HideInInspector] public bool bOnWallBackT;
        [HideInInspector] public bool bOnWallBackB;
        [HideInInspector] public bool bOnWallBack;
        [HideInInspector] public bool bOnLedgeHorizontal;
        [HideInInspector] public bool bOnLedgeVertical;
        [HideInInspector] public bool bOnLedge;

        private bool m_tmp_bDetectFloor;
        private bool m_tmp_bHitFloor;
        private bool m_tmp_bHitCeil;
        private bool m_tmp_bHitWallFrontT;
        private bool m_tmp_bHitWallFrontB;
        private bool m_tmp_bHitWallBackT;
        private bool m_tmp_bHitWallBackB;
        private bool m_tmp_bDetectLedgeHorizontal = true;
        private bool m_tmp_bDetectLedgeVertical;

        public override void UpdateOrigins(Player player)
        {
            Bounds head = player.hCol.head.bounds;
            Bounds feet = player.hCol.feet.bounds;

            originFloor.position = new Vector2(feet.center.x, feet.min.y);
            originCeil.position = new Vector2(head.center.x, head.max.y);

            originWallLT.position = new Vector2(head.min.x, head.center.y);
            originWallRT.position = new Vector2(head.max.x, head.center.y);
            originWallLB.position = new Vector2(feet.min.x, feet.center.y);
            originWallRB.position = new Vector2(feet.max.x, feet.center.y);

            originLedgeLT.position = new Vector2(head.min.x, head.max.y);
            originLedgeRT.position = new Vector2(head.max.x, head.max.y);
            originLedgeLB.position = new Vector2(feet.min.x, feet.min.y);
            originLedgeRB.position = new Vector2(feet.max.x, feet.min.y);
        }

        public override void UpdateData(Player player)
        {
            float lx = player.lookDir.fx;

            m_tmp_bDetectFloor = TerrainSensor.CheckFloor(originFloor.transform.position, player.detectLength);
            m_tmp_bHitFloor = TerrainSensor.CheckFloor(originFloor.transform.position, player.hitLength);
            m_tmp_bHitCeil = TerrainSensor.CheckCeil(originCeil.transform.position, player.hitLength);

            if(lx > 0)
            {
                m_tmp_bHitWallFrontT = TerrainSensor.CheckWallFront(originWallRT.position, player.hitLength, lx);
                m_tmp_bHitWallFrontB = TerrainSensor.CheckWallFront(originWallRB.position, player.hitLength, lx);
                m_tmp_bHitWallBackT = TerrainSensor.CheckWallBack(originWallLT.position, player.hitLength, lx);
                m_tmp_bHitWallBackB = TerrainSensor.CheckWallBack(originWallLB.position, player.hitLength, lx);

                m_tmp_bDetectLedgeHorizontal = TerrainSensor.CheckLedgeHorizontal(originLedgeRT.position, player.detectLength, lx);
                m_tmp_bDetectLedgeVertical = TerrainSensor.CheckLedgeVerticalDown(originLedgeRT.position + Vector3.right * player.ledgerp, player.detectLength * player.ledgeVerticalLengthWeight);
            }
            else if(lx < 0)
            {
                m_tmp_bHitWallBackT = TerrainSensor.CheckWallBack(originWallRT.position, player.hitLength, lx);
                m_tmp_bHitWallBackB = TerrainSensor.CheckWallBack(originWallRB.position, player.hitLength, lx);
                m_tmp_bHitWallFrontT = TerrainSensor.CheckWallFront(originWallLT.position, player.hitLength, lx);
                m_tmp_bHitWallFrontB = TerrainSensor.CheckWallFront(originWallLB.position, player.hitLength, lx);

                m_tmp_bDetectLedgeHorizontal = TerrainSensor.CheckLedgeHorizontal(originLedgeLT.position, player.detectLength, lx);
                m_tmp_bDetectLedgeVertical = TerrainSensor.CheckLedgeVerticalDown(originLedgeLT.position - Vector3.right * player.ledgerp, player.detectLength * player.ledgeVerticalLengthWeight);
            }

            bOnDetectFloor = m_tmp_bDetectFloor;
            bOnFloor = m_tmp_bHitFloor;
            bOnCeil = m_tmp_bHitCeil;
            bOnWallFrontT = m_tmp_bHitWallFrontT;
            bOnWallFrontB = m_tmp_bHitWallFrontB;
            bOnWallFront = m_tmp_bHitWallFrontT && m_tmp_bHitWallFrontB && m_tmp_bDetectLedgeHorizontal;
            bOnWallBackT = m_tmp_bHitWallBackT;
            bOnWallBackB = m_tmp_bHitWallBackB;
            bOnWallBack = m_tmp_bHitWallBackT && m_tmp_bHitWallBackB;
            bOnLedgeHorizontal = m_tmp_bDetectLedgeHorizontal;
            bOnLedgeVertical = m_tmp_bDetectLedgeVertical;
            bOnLedge = !m_tmp_bDetectLedgeHorizontal && m_tmp_bDetectLedgeVertical;
        }

        public override void UpdateMoveDir(Player player)
        {
            RaycastHit2D terrain = Physics2D.Raycast(originFloor.position, Vector2.down, player.hitLength, 1 << LayerMask.NameToLayer("Terrain"));
            player.moveDir.x = 1.0f;

            if(terrain.normal.y == 0)
                player.moveDir.y = 0;
            else
                player.moveDir.y = -terrain.normal.x / terrain.normal.y;
        }
    }
}