using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class BattleModule : MonoBehaviour
    {
        public EntityBase owner => m_owner;
        private EntityBase m_owner;

        public Transform damageParent;
        public TestDamageUI hitUI;
        public TestDamageUI healUI;

        private Queue<BattleSkill> m_reservedSkills;

        private void OnValidate()
        {
            TryGetComponent<EntityBase>(out m_owner);
        }

        private void Start()
        {
            m_reservedSkills = new Queue<BattleSkill>(4);
        }

        public void Reserve(BattleSkill skill, int count = 1)
        {
            if(count < 1)
                count = 1;
            for(int i = 0; i < count; ++i)
                m_reservedSkills.Enqueue(skill);
        }

        public void ExecuteReserved()
        {
            if(m_reservedSkills.Count > 0)
                ExecuteImmediate(m_reservedSkills.Dequeue());
        }

        public void ExecuteImmediate(BattleSkill skill)
        {
            EntityBase[] targets = skill.GetTargets(owner);

            int cnt = targets?.Length ?? 0;
            for(int i = 0; i < cnt; ++i)
                m_GiveDamage(targets[i], skill);
        }

        public void Clear()
        {
            m_reservedSkills.Clear();
        }

        private void m_GiveDamage(EntityBase target, BattleSkill skill)
        {
            // 게임에 진심인 사람은
            // 나만의 전투 공식을 만드는 데
            // 로망을 가지고 있다.
            /*
            [용어 설명]
            owner: 전투 모듈을 가지고 있는 Entity (공격자)
            target: 스킬 범위에 의해 감지된 Entity (피해자)
            [전투 공식]
            (최종데미지) = (공격자의 공격력) * (스킬 계수) - (피해자의 방어력)
            단, (최종데미지)의 최소값은 1이다.
            */
            float finalStrength = owner.strength.finalValue;
            finalStrength *= skill.baseDamage;
            float finalDefence = target.defence.finalValue;
            float finalDamage = finalStrength - finalDefence;

            if(finalDamage < 1)
                finalDamage = 1;

            target.Damage(finalDamage);
            Debug.Log("Damage Given.");
            // TestDamageUI ui = TestDamageUI.Get(hitUI, dH, target.transform.position);
            // ui.transform.SetParent(damageParent, false);
        }
    }
}