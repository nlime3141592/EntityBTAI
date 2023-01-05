using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityPlayerFSM : FiniteStateMachineNodeBT<EntityPlayer>
    {
        public int pageIndex { get; private set; }
        private PlayerTerrainCheckPage m_terrainPage;

        private bool m_bDF, m_bHF, m_bHW, m_bDL;

        public EntityPlayerFSM(
            ConfigurationBT<EntityPlayer> config, int id, string name,
            PlayerTerrainCheckPage terrainPage)
        : base(config, id, name, 5)
        {
            m_terrainPage = terrainPage;
        }

        protected override void p_OnPreInvokeNode()
        {
            m_terrainPage.Invoke();

            m_bDF = m_terrainPage[EPlayerTerrainCheckResult.Floor].bDetected;
            m_bHF = m_terrainPage[EPlayerTerrainCheckResult.Floor].bHit;
            m_bHW =
                (
                    m_terrainPage[EPlayerTerrainCheckResult.WallLT].bHit &&
                    m_terrainPage[EPlayerTerrainCheckResult.WallLB].bHit &&
                    m_terrainPage[EPlayerTerrainCheckResult.LedgeFloorLT].bDetected &&
                    config.instance.lookDir.x == -1
                ) ||
                (
                    m_terrainPage[EPlayerTerrainCheckResult.WallRT].bHit &&
                    m_terrainPage[EPlayerTerrainCheckResult.WallRB].bHit &&
                    m_terrainPage[EPlayerTerrainCheckResult.LedgeFloorRT].bDetected &&
                    config.instance.lookDir.x == 1
                );
            m_bDL =
                (
                    m_terrainPage[EPlayerTerrainCheckResult.LedgeLT].bDetected &&
                    config.instance.lookDir.x == -1
                ) ||
                (
                    m_terrainPage[EPlayerTerrainCheckResult.LedgeRT].bDetected &&
                    config.instance.lookDir.x == 1
                );
        }

        protected override int GetNextChildIndex()
        {
            int code = m_CreateTerrainCode(m_bDF, m_bHF, m_bHW, m_bDL);
            int index = m_ParsePage(code);

            // index = 1;

            pageIndex = index;
            if(index == 0) m_OnAir();
            else if(index == 1) m_OnFloor();
            else if(index == 2) m_OnWall();
            else if(index == 3) m_OnLedge();
            else if(index == 4) m_OnAbility();

            return index;
        }

        private int m_CreateTerrainCode(bool bDF, bool bHF, bool bHW, bool bDL)
        {
            int code = 0;
            if(bDF) code += 8;
            if(bHF) code += 4;
            if(bHW) code += 2;
            if(bDL) code += 1;
            return code;
        }

        // m_ParsePage(int) return value;
        //  0: 공중
        //  1: 바닥
        //  2: 벽
        //  3: 난간
        //  -1: 오류
        private int m_ParsePage(int code)
        {
            if((code & 0xfffffff0) != 0) return -1;
            else if(config.instance.bOnHoldLedge) return 3;
            else if(code == 1) return 3;
            else if(code < 4) return code;
            else return (code >> 2) - 2;
        }

        private void m_OnFloor()
        {
            // 바닥에서의 움직임 방향 설정
            RaycastHit2D floor = m_terrainPage[EPlayerTerrainCheckResult.Floor].terrain;
            config.instance.moveDir = new Vector2(1.0f, -floor.normal.x / floor.normal.y);
        }

        private void m_OnAir()
        {
            config.instance.moveDir = Vector2.right * config.instance.axisInput.x;
        }

        private void m_OnWall()
        {

        }

        private void m_OnLedge()
        {

        }

        private void m_OnAbility()
        {

        }
    }
}