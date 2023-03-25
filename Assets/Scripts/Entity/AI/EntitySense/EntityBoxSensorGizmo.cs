using UnityEngine;

namespace Unchord
{
    public sealed class EntityBoxSensorGizmo : EntitySensorGizmo
    {
        private Vector2 m_pCenter;
        private Vector2 m_size;

        public EntityBoxSensorGizmo(
            float lifeTime, Color color,
            Vector2 pStart, Vector2 pEnd)
        : base(lifeTime, color)
        {
            this.m_pCenter = (pStart + pEnd) / 2;
            this.m_size = (pEnd - pStart);
        }

        protected override void p_DrawGizmo()
        {
            Gizmos.DrawWireCube(m_pCenter, m_size);
        }
    }
}