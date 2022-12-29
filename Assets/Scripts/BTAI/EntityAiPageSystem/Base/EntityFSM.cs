namespace UnchordMetroidvania
{
    public sealed class EntityFSM : EntityAiPage
    {
        public bool bDetectFloor;
        public bool bHitFloor;
        public bool bDetectCeil;
        public bool bHitCeil;
        public bool bDetectWall;
        public bool bHitWall;
        public bool bDetectLedge;
        public bool bHitLedge;

        private EntityAiPage[] m_pages;

        public EntityFSM()
        {
            m_pages = new EntityAiPage[7];
        }

        public override InvokeResult Invoke(long curFps)
        {
            m_pages[6].Invoke(curFps);

            if(
                m_pages[5] != null && // TEST LINE!!
                m_pages[5].Invoke(curFps) == InvokeResult.FAIL)
                return InvokeResult.FAIL;

            int tCode = m_CreateTerrainCode(bDetectFloor, bHitFloor, bHitWall, bHitLedge);
            int pageIdx = m_ParsePage(tCode);

            return m_pages[pageIdx].Invoke(curFps);
            // return m_pages[1].Invoke();
        }

        private int m_CreateTerrainCode(bool bDF, bool bHF, bool bHW, bool bHL)
        {
            int code = 0;
            if(bDF) code += 8;
            if(bHF) code += 4;
            if(bHW) code += 2;
            if(bHL) code += 1;
            return code;
        }

        private int m_ParsePage(int code)
        {
            if((code & 0xfffffff0) != 0) return -1;
            else if(code == 1) return 3;
            else if(code < 4) return code;
            else return (code >> 2) - 2;
        }

        public void SetAirPage(EntityAirPage page) => m_pages[0] = page;
        public void SetFloorPage(EntityFloorPage page) => m_pages[1] = page;
        public void SetWallPage(EntityWallPage page) => m_pages[2] = page;
        public void SetLedgePage(EntityLedgePage page) => m_pages[3] = page;
        public void SetCeilPage(EntityCeilPage page) => m_pages[4] = page;
        public void SetAbilityPage(EntityAbilityPage page) => m_pages[5] = page;
        public void SetTerrainCheckPage(EntityTerrainCheckPage page) => m_pages[6] = page;
    }
}