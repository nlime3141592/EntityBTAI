using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [DisallowMultipleComponent]
    public class SkillModule : StateEventHandler<ISkillEvent>
    {
        public List<string> tags;
        public LayerMask mask;

        public bool bIgnoreSelf = true;

        private List<Collider2D> m_sensorBuffer;
        private List<Entity> m_targets;

        protected override void Awake()
        {
            base.Awake();

            m_sensorBuffer = new List<Collider2D>(1);
            m_targets = new List<Entity>(1);
        }

        public SkillModule Reset()
        {
            m_sensorBuffer.Clear();
            m_targets.Clear();
            return this;
        }

        public SkillModule SenseColliders(AreaSensor _sensor)
        {
            Entity moduleOwner = baseComponent.baseComponent;

            moduleOwner.transform.BindLocal(_sensor.transform);
            _sensor.OnUpdate();
            _sensor.Sense(in m_sensorBuffer, tags, mask);

            if(bIgnoreSelf)
            {
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

        public float GetStandardDamage(Entity _attacker, Entity _victim, float _weight)
        {
            if(_victim.bInvincibility) // NOTE: 무적
                return 0;
            else if(_victim.fixedTakenDamage.finalValue > 0) // NOTE: 고정 피해량
                return _victim.fixedTakenDamage.finalValue;

            // NOTE: 순수 데미지 연산
            float pureDamage = _attacker.strength.finalValue - _victim.defence.finalValue;

            if(pureDamage <= 0)
                return 0;

            float finalDamage = pureDamage;

            // NOTE: 크리티컬 확률 발동 및 크리티컬 데미지 연산
            float isCritical = UnityEngine.Random.value;
            if(isCritical < _attacker.criticalChance.finalValue)
                finalDamage *= (2.0f + Utilities.Max<float>(0, _attacker.criticalDamage.finalValue));

            // NOTE: 최종 데미지 증가
            finalDamage *= (1.0f + Utilities.Max<float>(0, _attacker.finalDamage.finalValue));
            
            return Utilities.Max<float>(1, finalDamage);
        }

        public SkillModule TakeStandardDamage(Entity _victim, float _weight)
        {
            Entity moduleOwner = baseComponent.baseComponent;
            float finalDamage = GetStandardDamage(moduleOwner, _victim, _weight);
            _victim.ChangeHealth(-finalDamage);
            return this;
        }

        public bool TryGroggy(Entity _victim)
        {
            Entity moduleOwner = baseComponent.baseComponent;
            int sumDirX = moduleOwner.lookDir.ix + _victim.lookDir.ix;
            int sumDirY = moduleOwner.lookDir.iy + _victim.lookDir.iy;

            if(_victim.bParrying && sumDirX * sumDirY == 0)
            {
                moduleOwner.groggyValue = Utilities.Min<float>(moduleOwner.maxGroggyValue.finalValue, moduleOwner.groggyValue + _victim.groggyStrength.finalValue);
                return true;
            }

            return false;
        }

        public void OnSkill()
        {
            base.UpdateEventListener();
            iEvListener?.OnSkill(this);
        }
    }
}