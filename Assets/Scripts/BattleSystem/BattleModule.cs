using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class BattleModule : MonoBehaviour
    {
        public _EntityBase owner { get; private set; }

        private void OnValidate()
        {
            owner = GetComponent<_EntityBase>();
        }

        public void UseBattleSkill(BattleSkill skill)
        {
            _EntityBase[] targets = skill.GetTargets(owner);

            for(int i = 0; i < targets.Length; ++i)
                m_GiveDamage(targets[i]);
        }

        private void m_GiveDamage(_EntityBase target)
        {
            // 게임에 진심인 사람은
            // 나만의 전투 공식을 만드는 데
            // 로망을 가지고 있다.
            /*
            [용어 설명]
            owner: 전투 모듈을 가지고 있는 Entity (공격자)
            target: 스킬 범위에 의해 감지된 Entity (피해자)
            [전투 공식]
            (최종데미지) = (공격자의 공격력) - (피해자의 방어력)
            단, (최종데미지)의 최소값은 1이다.
            */
            float finalStrength = owner.strength.finalValue;
            float finalDefence = target.defence.finalValue;
            float finalDamage = finalStrength - finalDefence;

            if(finalDamage < 1)
                finalDamage = 1;

            target.SetHealth(target.health - finalDamage);
        }
    }
}