using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(Entity))]
    [DisallowMultipleComponent]
    public class BattleModule : MonoBehaviour
    {
        public Entity owner => m_owner;
        private Entity m_owner;

        public Transform damageParent;
        // public TestDamageUI hitUI;
        // public TestDamageUI healUI;

        public EntitySensorGizmoOption battleRangeGizmo;
        public int targetLayerMask;

        private IBattleState m_battleState;
        private List<Collider2D> m_ignores;

        private void OnValidate()
        {
            TryGetComponent<Entity>(out m_owner);
        }

        private void Start()
        {
            TryGetComponent<Entity>(out m_owner);
        }

        public void SetBattleState(IBattleState state)
        {
            m_battleState = state;
        }

        public void SetIgnoreColliders(List<Collider2D> ignores)
        {
            m_ignores = ignores;
        }

        public void ClearBattleState()
        {
            m_battleState = null;
        }

        // 새로운 애니메이션 이벤트 트리거
        public void _TriggerBattleState()
        {
            IStateBase tmpState = owner.machineInterface.state;

            if(!(tmpState is IBattleState))
                return;
            else if(!System.Object.Equals(tmpState, m_battleState))
                m_battleState = tmpState as IBattleState;
        }

        // 새로운 데미지 공식
        private float m_GetDamage(Entity executor, Entity target)
        {
            if(target.bInvincibility) // 무적
                return 0;
            else if(target.fixedTakenDamage.finalValue > 0) // Target Entity가 입는 고정 피해
                return target.fixedTakenDamage.finalValue;
            else if(executor.healthProportionDamage.finalValue > 0) // Executor Entity가 가하는 체력 비례 데미지
                return target.maxHealth.finalValue * executor.healthProportionDamage.finalValue;

            float baseDamage = executor.strength.finalValue - target.defence.finalValue;

            if(baseDamage <= 0) // 기초 데미지가 0 이하인 경우
                return 0;

            float finalDamage = baseDamage;

            // 크리티컬
            float criticalRatio = UnityEngine.Random.value;
            if(criticalRatio < executor.criticalChance.finalValue)
                finalDamage *= (2.0f + Utilities.Max<float>(0, executor.criticalDamage.finalValue));

            // 최종 데미지 증가
            finalDamage *= (1.0f + Utilities.Max<float>(0, executor.finalDamage.finalValue));

            return finalDamage;
        }

        public void TriggerBattleState()
        {
            Entity attacker = m_battleState.attacker;
            List<Entity> targets = m_battleState.targets;
            LTRB range = m_battleState.range;
            int targetCount = m_battleState.targetCount;
            float baseDamage = m_battleState.baseDamage;
            bool bGetGroggyValue = false;

            Collider2D[] colTargets = EntitySensor.OverlapBox(attacker, range, battleRangeGizmo, targetLayerMask);
            targets.Clear();
            targets
                .FilterFromColliders(attacker, colTargets, false, m_ignores)
                .SetTargetCount(targetCount);

            foreach(Entity target in targets)
            {
                float finalDamage = this.GetFinalDamage(target, baseDamage);

                bGetGroggyValue |= target.bParrying;

                if(!target.bParrying)
                    target.ChangeHealth(-finalDamage);
            }

            if(bGetGroggyValue)
            {
                // attacker.groggyValue += attacker.baseMentality.finalValue;
                attacker.groggyValue += 0.34f;
            }
        }

        public float GetFinalDamage(Entity target, float baseDamage)
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
            else if(target.fixedTakenDamage.finalValue > 0)
                return target.fixedTakenDamage.finalValue;

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

        public float GetFinalGroggy(Entity target)
        {
            float finalMental = target.mentality.finalValue;

            if(finalMental < 0.001f)
                return 0.001f;

            return finalMental;
        }
    }
}