using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Box Sensor", menuName = "Unchord2D/Sensor/Box", order = 3)]
    public class BoxSensor_SO : Sensor_SO
    {
        [Header("Box Sensor Options")]
        public float l = 0.5f;
        public float t = 0.5f;
        public float r = 0.5f;
        public float b = 0.5f;

        private float m_ltx, m_lty;
        private float m_rtx, m_rty;
        private float m_lbx, m_lby;
        private float m_rbx, m_rby;
        private Vector2 m_lt, m_rt, m_lb, m_rb;
        private Vector2 m_boxCenter, m_boxSize;
        private float m_boxAngle;

        protected override void Overlap(in List<Collider2D> _colliders, int _layerMask)
        {
            m_UpdateOrigins();

            Collider2D[] sensed = Physics2D.OverlapBoxAll(m_boxCenter, m_boxSize, m_boxAngle, _layerMask);
            AddSensedColliders(_colliders, sensed);
        }

        private void m_UpdateOrigins()
        {
            TransformManager2.GetGlobalPosition(out m_ltx, out m_lty, transform, -l, t);
            TransformManager2.GetGlobalPosition(out m_rtx, out m_rty, transform, r, t);
            TransformManager2.GetGlobalPosition(out m_lbx, out m_lby, transform, -l, -b);
            TransformManager2.GetGlobalPosition(out m_rbx, out m_rby, transform, r, -b);

            m_lt.Set(m_ltx, m_lty);
            m_rt.Set(m_rtx, m_rty);
            m_lb.Set(m_lbx, m_lby);
            m_rb.Set(m_rbx, m_rby);

            m_boxSize.Set(r + l, b + t);
            m_boxCenter.Set(0.5f * (m_lb.x + m_rt.x), 0.5f * (m_lb.y + m_rt.y));
            m_boxAngle = Vector2.SignedAngle(Vector2.right, m_rt - m_lt);
        }

#if UNITY_EDITOR
        protected override void Draw()
        {
            m_UpdateOrigins();

            Gizmos.DrawLine(m_lt, m_rt);
            Gizmos.DrawLine(m_lt, m_lb);
            Gizmos.DrawLine(m_rb, m_lb);
            Gizmos.DrawLine(m_rb, m_rt);
        }
#endif
    }
}