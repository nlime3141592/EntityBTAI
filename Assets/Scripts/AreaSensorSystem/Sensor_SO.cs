using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public abstract class Sensor_SO : ScriptableObject
    {
        public Transform2 transform;
        public List<Sensor_SO> children;

        public static T NewSensor<T>()
        where T : Sensor_SO, new()
        {
            return new T();
        }

        public void Sense(in List<Collider2D> _colliders, int _layerMask)
        {
            m_rec_OnUpdate(null, this);
            m_rec_Sense(_colliders, null, this, _layerMask);
        }

        protected abstract void Overlap(in List<Collider2D> _colliders, int _layerMask);

#region MonoBehaviour.Update()
        // NOTE: for debugging.
        public void OnUpdate()
        {
            m_rec_OnUpdate(null, this);
        }
#endregion

        protected void AddSensedColliders(in List<Collider2D> _colliders, Collider2D[] _sensed)
        {
            int length = _sensed.Length;

            for(int i = 0; i < length; ++i)
                if(!_colliders.Contains(_sensed[i]))
                    _colliders.Add(_sensed[i]);
        }

        private void m_rec_Sense(in List<Collider2D> _colliders, Sensor_SO _parent, Sensor_SO _child, int _layerMask)
        {
            if(_parent == null)
                TransformManager2.UnsafeSyncRootBasis(_child.transform);
            else
                TransformManager2.UnsafeSyncBasis(_child.transform, _parent.transform);

            int count = _child.children?.Count ?? 0;

            for(int i = 0; i < count; ++i)
                m_rec_Sense(_colliders, _child, _child.children[i], _layerMask);

            _child.Overlap(_colliders, _layerMask);
        }

        private void m_rec_OnUpdate(Sensor_SO _parent, Sensor_SO _child)
        {
            if(_parent == null)
                TransformManager2.UnsafeSyncRootBasis(_child.transform);
            else if(_child == null)
                return;
            else
                TransformManager2.UnsafeSyncBasis(_child.transform, _parent.transform);

            int count = _child.children?.Count ?? 0;

            for(int i = 0; i < count; ++i)
                m_rec_OnUpdate(_child, _child.children[i]);
        }

#if UNITY_EDITOR
#region MonoBehaviour.OnDrawGizmos()
        public void DrawSensor(Color _color)
        {
            m_rec_OnUpdate(null, this);
            Gizmos.color = _color;
            m_rec_DrawGizmos();
        }

        public void DrawBasis(Color _px, Color _py, Color _nx, Color _ny)
        {
            m_rec_DrawBasis(_px, _py, _nx, _ny);
        }

        protected abstract void Draw();

        private void m_rec_DrawGizmos()
        {
            Draw();

            int count = children?.Count ?? 0;

            for(int i = 0; i < count; ++i)
                children[i]?.m_rec_DrawGizmos();
        }

        private void m_rec_DrawBasis(Color _px, Color _py, Color _nx, Color _ny)
        {
            m_DrawBasis(_px, _py, _nx, _ny);

            int count = children?.Count ?? 0;

            for(int i = 0; i < count; ++i)
                children[i]?.m_rec_DrawBasis(_px, _py, _nx, _ny);
        }

        private void m_DrawBasis(Color _px, Color _py, Color _nx, Color _ny)
        {
            float ox, oy; // origin
            float x1, y1; // x axis basis
            float x2, y2; // y axis basis
            float x3, y3; // neg x axis
            float x4, y4; // neg y axis

            TransformManager2.GetGlobalPosition(out ox, out oy, transform, 0, 0);
            TransformManager2.GetGlobalPosition(out x1, out y1, transform, transform.gsx, 0);
            TransformManager2.GetGlobalPosition(out x2, out y2, transform, 0, transform.gsy);
            TransformManager2.GetGlobalPosition(out x3, out y3, transform, -transform.gsx, 0);
            TransformManager2.GetGlobalPosition(out x4, out y4, transform, 0, -transform.gsy);

            Vector2 origin = new Vector2(ox, oy);
            Vector2 xAxis = new Vector2(x1, y1);
            Vector2 yAxis = new Vector2(x2, y2);
            Vector2 nxAxis = new Vector2(x3, y3);
            Vector2 nyAxis = new Vector2(x4, y4);

            Color tmp = Gizmos.color;
            Gizmos.color = _px;
            Gizmos.DrawLine(origin, xAxis);
            Gizmos.color = _py;
            Gizmos.DrawLine(origin, yAxis);
            Gizmos.color = _nx;
            Gizmos.DrawLine(origin, nxAxis);
            Gizmos.color = _ny;
            Gizmos.DrawLine(origin, nyAxis);
            Gizmos.color = tmp;
        }
#endregion
#endif
    }
}