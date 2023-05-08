using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public sealed class AreaSensorBox : AreaSensor
    {
        public ShapeBox box;

        private Vector2 m_center;
        private Vector2 m_size;

        public override void Sense(in List<Collider2D> _colliders, in List<string> _tags, LayerMask _mask)
        {
            box.Sync(transform);

            m_center.Set(box.cx, box.cy);
            m_size.Set(box.sx, box.sy);

            Collider2D[] sensed = Physics2D.OverlapBoxAll(m_center, m_size, box.deg, _mask);
            AddSensedColliders(_colliders, _tags, sensed);
        }

        public override void DebugSensor(Color _color, float _duration)
        {
            base.DebugSensor(_color, _duration);

            Debug.DrawLine(new Vector2(box.ltx, box.lty), new Vector2(box.rtx, box.rty), _color, _duration);
            Debug.DrawLine(new Vector2(box.ltx, box.lty), new Vector2(box.lbx, box.lby), _color, _duration);
            Debug.DrawLine(new Vector2(box.rbx, box.rby), new Vector2(box.rtx, box.rty), _color, _duration);
            Debug.DrawLine(new Vector2(box.rbx, box.rby), new Vector2(box.lbx, box.lby), _color, _duration);
        }
    }
}