using UnityEngine;

namespace UnchordMetroidvania
{
    public class _EntityPlayerFSM : FiniteStateMachineNodeBT<EntityPlayer>
    {
        private _EntityPlayerTerrainCheckPage m_terrainPage;

        private bool m_bDF, m_bHF, m_bHW, m_bDL;

        public _EntityPlayerFSM(
            ConfigurationBT<EntityPlayer> config, int id, string name,
            _EntityPlayerTerrainCheckPage terrainPage)
        : base(config, id, name, 7)
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
                    config.instance.lookDir.x == -1
                ) ||
                (
                    m_terrainPage[EPlayerTerrainCheckResult.WallRT].bHit &&
                    m_terrainPage[EPlayerTerrainCheckResult.WallRB].bHit &&
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
            Debug.Log(string.Format("Page: {0}", index));

            // return index;
            return 0;
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

        private int m_ParsePage(int code)
        {
            if((code & 0xfffffff0) != 0) return -1;
            else if(code == 1) return 3;
            else if(code < 4) return code;
            else return (code >> 2) - 2;
        }
    }
}