using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    public sealed class PlainChecker // : TerrainChecker
    {
        public Transform tOrigin;
        public Vector2 globalOffset;
        public float detectLength;
        public float hitLength;

        private Vector2 m_direction;

        public static PlainChecker GetLeftWall(ConfigurationBT<TerrainCheckResult> config, string name)
        => new PlainChecker(config, 0, name, Vector2.left);

        public static PlainChecker GetCeil(ConfigurationBT<TerrainCheckResult> config, string name)
        => new PlainChecker(config, 1, name, Vector2.up);

        public static PlainChecker GetRightWall(ConfigurationBT<TerrainCheckResult> config, string name)
        => new PlainChecker(config, 2, name, Vector2.right);

        public static PlainChecker GetFloor(ConfigurationBT<TerrainCheckResult> config, string name)
        => new PlainChecker(config, 3, name, Vector2.down);

        private PlainChecker(ConfigurationBT<TerrainCheckResult> config, int id, string name, Vector2 direction)
        // : base(config, id, name)
        {
            m_direction = direction;
        }
/*
        protected override InvokeResult p_Invoke()
        {
            Vector2 origin = (Vector2)tOrigin.position + globalOffset;
            int layerMask = p_GetLayerMask();

            RaycastHit2D hit = Physics2D.Raycast(origin, m_direction, detectLength, layerMask);
            bool bDetected = hit;
            bool bHit = bDetected && hit.distance <= hitLength;

            // For Debugging.
                Debug.DrawLine(origin, origin + m_direction * detectLength);

            config.instance.bDetected = bDetected;
            config.instance.bHit = bHit;
            config.instance.terrain = hit;

            return InvokeResult.Success;
        }
*/
    }
}