using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Circle Sensor", menuName = "Unchord2D/Sensor/Circle", order = 4)]
    public class CircleSensor_SO : Sensor_SO
    {
        [Header("Circle Sensor Options")]
        public float radius = 0.5f;

        private Vector2 m_gPosition;
        private float m_radius;

        protected override void Overlap(in List<Collider2D> _colliders, int _layerMask)
        {
            m_UpdateOrigins();

            Collider2D[] sensed = Physics2D.OverlapCircleAll(m_gPosition, m_radius, _layerMask);
            AddSensedColliders(_colliders, sensed);
        }

        private void m_UpdateOrigins()
        {
            float min(float a, float b) => a < b ? a : b;

            m_gPosition.Set(transform.gpx, transform.gpy);
            m_radius = radius * min(transform.gsx, transform.gsy);
            // m_radius = radius * Utilities.Min<float>(transform.gsx, transform.gsy);
        }

#if UNITY_EDITOR
        protected override void Draw()
        {
            m_UpdateOrigins();
            Gizmos.DrawWireSphere(m_gPosition, m_radius);
        }
#endif
    }
}