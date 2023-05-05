using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public sealed class AreaSensorCircle : AreaSensor
    {
        public ShapeCircle circle;

        private Vector2 m_center;

        public override void Sense(in List<Collider2D> _colliders, in List<string> _tags, LayerMask _mask)
        {
            circle.Sync(transform);

            m_center.Set(circle.cx, circle.cy);

            Collider2D[] sensed = Physics2D.OverlapCircleAll(m_center, circle.r, _mask);
            AddSensedColliders(_colliders, _tags, sensed);
        }

        public override void DebugSensor(Color _color, float _duration)
        {
            base.DebugSensor(_color, _duration);

            Vector2 center = new Vector2(circle.cx, circle.cy);

            Debug.DrawRay(center, Vector2.right * circle.radius, _color, _duration);
            Debug.DrawRay(center, Vector2.left * circle.radius, _color, _duration);
            Debug.DrawRay(center, Vector2.up * circle.radius, _color, _duration);
            Debug.DrawRay(center, Vector2.down * circle.radius, _color, _duration);
        }
    }
}