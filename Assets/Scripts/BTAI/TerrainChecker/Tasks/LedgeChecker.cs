/*
using UnityEngine;

namespace UnchordMetroidvania
{
    public class LedgeChecker : TerrainChecker
    {
        public int xDir { get; private set; }
        public int yDir { get; private set; }

        public LedgeChecker(ITerrainCheckerConfig config, int xDir, int yDir)
        : base(config, -1, "LedgeChecker")
        {
            this.xDir = xDir < 0 ? -1 : 1;
            this.yDir = yDir < 0 ? -1 : 1;
        }

        public override InvokeResult Invoke()
        {
            Vector2 origin = p_config.tOrigin.position;
            Vector2 dirX = Vector2.right * xDir;
            Vector2 dirY = Vector2.up * yDir;
            Vector2 dLedge = dirX * p_config.ledgerp;
            float dLength = p_config.dLength;
            float hLength = p_config.hLength;
            float height = p_config.height;
            int layerMask = p_config.layerMask;

            RaycastHit2D tempHit = Physics2D.Raycast(origin, dirX, dLength, layerMask);

            if(tempHit)
            {
                p_config.terrain = default(RaycastHit2D);
                p_config.bDetected = false;
                p_config.bHit = false;
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(origin + dLedge, dirY, height, layerMask);
                bool bDetected = hit;
                bool bHit = bDetected && hit.distance <= hLength;

                p_config.terrain = hit;
                p_config.bDetected = bDetected;
                p_config.bHit = bHit;
            }

            return InvokeResult.SUCCESS;
        }
    }
}
*/