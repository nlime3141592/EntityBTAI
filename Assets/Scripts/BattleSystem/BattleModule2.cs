using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(Entity))]
    [DisallowMultipleComponent]
    // 스킬 컴포넌트
    public class BattleModule2 : MonoBehaviour
    {
        public Entity entity => m_entity;
        private Entity m_entity;

        private void Start()
        {
            TryGetComponent<Entity>(out m_entity);
        }

        // 1. 공격 스킬
        // 데미지 공식에 의한 최종 데미지 계산
        public float GetDamageByFormular(Entity _attacker, Entity _victim, float _baseDamage, float _weight)
        {
            // NOTE: 데미지 공식을 이 곳에 집어넣는다.
            return _baseDamage * _weight;
        }

        public float TakeDamageByFormular(Entity _attacker, Entity _victim, float _baseDamage, float _weight)
        {
            float damage = -GetDamageByFormular(_attacker, _victim, _baseDamage, _weight);
            float before = _victim.health;

            return _victim.ChangeHealth(damage) - before;
        }

        // 최대 체력 비례 데미지
        public float GetDamageByMaxHealth(float _maxHealth, float _percentDiv100)
        {
            return _maxHealth * _percentDiv100;
        }

        public float TakeDamageByMaxHealth(Entity _target, float _percentDiv100)
        {
            float damage = -GetDamageByMaxHealth(_target.maxHealth.finalValue, _percentDiv100);
            float before = _target.health;

            return _target.ChangeHealth(damage) - before;
        }

        // 2. 회복 스킬
        // 3. 그로기 제공
    }
}