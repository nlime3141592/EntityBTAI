using System;
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

        public EntitySensorGizmoOption battleRangeGizmo;

        private IBattleState m_battleState;

        private void OnValidate()
        {
            TryGetComponent<EntityBase>(out m_owner);
        }

        private void Start()
        {
            TryGetComponent<EntityBase>(out m_owner);
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
            EntityBase attacker = m_battleState.attacker;
            List<EntityBase> targets = m_battleState.targets;
            LTRB range = m_battleState.range;
            int targetCount = m_battleState.targetCount;
            float baseDamage = m_battleState.baseDamage;
            bool bGetGroggyValue = false;

            Collider2D[] colTargets = EntitySensor.OverlapBox(attacker, range, battleRangeGizmo);
            targets.Clear();
            targets
                .FilterFromColliders(attacker, colTargets, false)
                .SetTargetCount(targetCount);

            foreach(EntityBase target in targets)
            {
                float finalDamage = this.GetFinalDamage(target, baseDamage);

                bGetGroggyValue |= target.bParrying;

                if(!target.bParrying)
                    target.Damage(finalDamage);
            }

            if(bGetGroggyValue)
                attacker.groggyValue += attacker.baseMentality.finalValue;
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
            if(target.bInvincibility)
                return 0;
            else if(target.fixTakenDamage > 0)
                return target.fixTakenDamage;

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

        public float GetFinalGroggy(EntityBase target)
        {
            float finalMental = target.baseMentality.finalValue;

            if(finalMental < 0.001f)
                return 0.001f;

            return finalMental;
        }
    }
} 