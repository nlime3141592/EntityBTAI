using UnityEngine;

namespace Unchord
{
    public abstract class EntitySensorGizmo
    {
        public EntitySensorGizmo next { get; internal set; }
        public EntitySensorGizmo prev { get; internal set; }
        private float m_lifeTime;
        private Color m_color;

        public EntitySensorGizmo(float lifeTime, Color color)
        {
            this.m_lifeTime = lifeTime;
            this.m_color = color;
        }

        public bool OnDrawGizmos(float deltaTime)
        {
            Gizmos.color = m_color;
            p_DrawGizmo();
            m_lifeTime -= deltaTime;
            return m_lifeTime > 0;
        }

        protected abstract void p_DrawGizmo();
    }
}