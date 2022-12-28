using UnityEngine;

namespace UnchordMetroidvania
{
    public class WallChecker : TerrainChecker
    {
        public int xDir { get; private set; }

        public WallChecker(ITerrainCheckerConfig config, int xDir)
        : base(config, -1, "WallChecker")
        {
            this.xDir = xDir < 0 ? -1 : 1;
        }

        public override InvokeResult Invoke()
        {
            Vector2 origin = p_config.tOrigin.position;
            Vector2 dir = Vector2.right * xDir;
            float dLength = p_config.dLength;
            float hLength = p_config.hLength;
            int layerMask = p_config.layerMask;

            RaycastHit2D hit = Physics2D.Raycast(origin, dir, dLength, layerMask);
            bool bDetected = hit;
            bool bHit = bDetected && hit.distance <= hLength;

            p_config.terrain = hit;
            p_config.bDetected = bDetected;
            p_config.bHit = bHit;

            return InvokeResult.SUCCESS;
        }
    }
}