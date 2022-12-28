using UnityEngine;

namespace UnchordMetroidvania
{
    public class CeilChecker : TerrainChecker
    {
        public CeilChecker(ITerrainCheckerConfig config)
        : base(config, -1, "CeilChecker")
        {

        }

        public override InvokeResult Invoke()
        {
            Vector2 origin = p_config.tOrigin.position;
            Vector2 dir = Vector2.up;
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