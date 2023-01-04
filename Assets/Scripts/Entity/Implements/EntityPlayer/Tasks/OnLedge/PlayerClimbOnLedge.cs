using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerClimbOnLedge : PlayerOnLedge
    {
        private bool m_bHoldLedge;
        private Vector2 m_holdPosition;
        private Vector2 m_endPosition;

        public PlayerClimbOnLedge(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {
            
        }

        private bool m_CanLedgeHold()
        {
            TerrainCheckResult ledgeLT = config.instance.terrainPage[EPlayerTerrainCheckResult.LedgeLT];
            TerrainCheckResult ledgeRT = config.instance.terrainPage[EPlayerTerrainCheckResult.LedgeRT];

            Transform origin = null;
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");
            Vector2 hitPos = Vector2.zero;
            Vector2 ledgePos = Vector2.zero;
            Vector2 dOrigin = Vector2.zero;
            Vector2 dCenter = Vector2.zero;
            m_bHoldLedge = false;
            float dX = 0.04f;
            float ix = config.instance.axisInput.x;

            if(ledgeLT.bDetected && ix < 0)
            {
                origin = config.instance.origin_LT;
                hitPos = ledgeLT.terrain.point;

                float x = Physics2D.Raycast(origin.position, Vector2.left, origin.position.x - hitPos.x, layerMask).point.x;
                float y = hitPos.y;

                ledgePos = new Vector2(x + dX, y);
                dOrigin = origin.position - config.instance.transform.position;

                config.curTask = this;
                m_holdPosition = ledgePos - dOrigin;

                dCenter = config.instance.transform.position - config.instance.origin_F.position;
                m_endPosition = ledgePos + dCenter + Vector2.left * 0.1f;

                m_bHoldLedge = true;
                config.instance.bOnHoldLedge = true;
                return true;
            }
            else if(ledgeRT.bDetected && ix > 0)
            {
                origin = config.instance.origin_RT;
                hitPos = ledgeRT.terrain.point;

                float x = Physics2D.Raycast(origin.position, Vector2.right, hitPos.x - origin.position.x, layerMask).point.x;
                float y = hitPos.y;

                ledgePos = new Vector2(x - dX, y);
                dOrigin = origin.position - config.instance.transform.position;

                config.curTask = this;
                m_holdPosition = ledgePos - dOrigin;

                dCenter = config.instance.transform.position - config.instance.origin_F.position;
                m_endPosition = ledgePos + dCenter + Vector2.right * 0.1f;

                m_bHoldLedge = true;
                config.instance.bOnHoldLedge = true;
                return true;
            }
            return false;
        }

        protected override InvokeResult p_Invoke()
        {
            if(!m_bHoldLedge && !m_CanLedgeHold())
                return InvokeResult.Failure;

            if(!config.instance.bOnLedgeEnd)
            {
                config.instance.FixConstraints(true, true);
                config.instance.transform.position = m_holdPosition;
                config.instance.velModule.SetVelocityXY(0.0f, 0.0f);
                return InvokeResult.Running;
            }
            else
            {
                config.instance.FixConstraints(false, false);
                config.instance.transform.position = m_endPosition;
                config.instance.bOnLedgeEnd = false;
                config.instance.bOnHoldLedge = false;
                m_bHoldLedge = false;
                return InvokeResult.Success;
            }
        }
    }
}