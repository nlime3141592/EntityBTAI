using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public abstract class AreaSensor
    {
        public Transform2 transform;

        public abstract void Sense(in List<Collider2D> _colliders, in List<string> _tags, LayerMask _mask);

        public void OnUpdate()
        {
            TransformManager2.SyncTransforms(this.transform);
        }

        public virtual void DebugSensor(Color _color, float _duration) {}

        protected void AddSensedColliders(in List<Collider2D> _colliders, in List<string> _tags, Collider2D[] _sensed)
        {
            int length = _sensed.Length;

            for(int i = 0; i < length; ++i)
            {
                if((_tags == null || _tags.Count == 0 || _tags.Contains(_sensed[i].gameObject.tag)) && !_colliders.Contains(_sensed[i]))
                    _colliders.Add(_sensed[i]);
            }
        }
    }
}