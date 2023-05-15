using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [DisallowMultipleComponent]
    public class SkillModule : StateEventTrigger<ISkillEvent>
    {
        public List<string> tags;
        public LayerMask mask;

        public bool bIgnoreSelf = true;

        private List<Collider2D> m_sensorBuffer;
        private List<Entity> m_targets;

        public SkillModule Reset()
        {
            m_sensorBuffer.Clear();
            m_targets.Clear();
            return this;
        }

        public SkillModule SenseColliders(AreaSensor _sensor)
        {
            _sensor.Sense(in m_sensorBuffer, tags, mask);

            if(bIgnoreSelf)
            {
                Entity moduleOwner = baseComponent.baseComponent;
                this.IgnoreColliders(moduleOwner.battleTriggers);
                this.IgnoreColliders(moduleOwner.volumeCollisions);
            }

            return this;
        }

        public SkillModule AddCollider(Collider2D _collider)
        {
            if(!m_sensorBuffer.Contains(_collider))
                m_sensorBuffer.Add(_collider);
            return this;
        }

        public SkillModule AddColliders(List<Collider2D> _colliders)
        {
            for(int i = 0; i < _colliders.Count; ++i)
                this.AddCollider(_colliders[i]);
            return this;
        }

        public SkillModule IgnoreCollider(Collider2D _ignore)
        {
            m_sensorBuffer.RemoveAll((collider) => collider == _ignore);
            return this;
        }

        public SkillModule IgnoreColliders(List<Collider2D> _ignores)
        {
            m_sensorBuffer.RemoveAll((collider) => _ignores.Contains(collider));
            return this;
        }

        public List<Entity> GetTargets()
        {
            m_sensorBuffer.GetComponents<Entity>(in m_targets);
            return m_targets;
        }

        public float GetStandardDamage(Entity _attacker, Entity _victim)
        {
            return 0;
        }

        public void OnSkill()
        {
            UpdateEventListener();
            iEventListener?.OnSkill(this);
        }
    }
}