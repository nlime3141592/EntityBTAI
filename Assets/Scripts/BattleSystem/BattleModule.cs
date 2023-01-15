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

        private IBattleState m_battleState;

        private void OnValidate()
        {
            TryGetComponent<EntityBase>(out m_owner);
        }

        private void Start()
        {

        }

        public void SetBattleState(IBattleState state)
        {
            m_battleState = state;
        }

        public void ClearBattleState()
        {
            m_battleState = null;
        }

        public void TriggerBattleState()
        {
            m_battleState?.OnBattle();
        }

        public float GetFinalDamage(EntityBase target, float baseDamage)
        {
            /*
            [용어 설명]
            owner: 전투 모듈을 가지고 있는 Entity (공격자)
            target: 스킬 범위에 의해 감지된 Entity (피해자)
            [전투 공식]
            (최종데미지) = (공격자의 공격력) * (스킬 계수) - (피해자의 방어력)
            단, (최종데미지)의 최소값은 1이다.
            */
            float finalStrength = owner.strength.finalValue;
            finalStrength *= baseDamage;
            float finalDefence = target.defence.finalValue;
            float finalDamage = finalStrength - finalDefence;

            if(finalDamage < 1)
                finalDamage = 1;

            return finalDamage;
            // TestDamageUI ui = TestDamageUI.Get(hitUI, dH, target.transform.position);
            // ui.transform.SetParent(damageParent, false);
        }
    }
}