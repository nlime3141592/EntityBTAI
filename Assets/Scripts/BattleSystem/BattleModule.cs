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

        // public Transform damageParent;
        // public TestDamageUI hitUI;
        // public TestDamageUI healUI;

        public EntitySensorGizmoOption battleRangeGizmo;
        public List<string> tags;
        public LayerMask mask;

        private List<Collider2D> m_ignores;

        private void OnValidate()
        {
            TryGetComponent<Entity>(out m_owner);
        }

        private void Start()
        {
            TryGetComponent<Entity>(out m_owner);
        }

        public void SetIgnoreColliders(List<Collider2D> ignores)
        {
            m_ignores = ignores;
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
            IStateBase current = owner.fsm.state;
            (current as IBattleState)?.OnTriggerBattleState(this);

            /*
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
            */
        }

        public static float GetFinalDamage(Entity _attacker, Entity _victim, float _baseDamage)
        {
            if(_victim.bInvincibility) // NOTE: 무적
                return 0;
            else if(_victim.fixedTakenDamage.finalValue > 0) // NOTE: 고정 피해량
                return _victim.fixedTakenDamage.finalValue;

            // NOTE: 순수 데미지
            float pureDamage = _attacker.strength.finalValue - _victim.defence.finalValue;

            if(pureDamage <= 0)
                return 0;

            float finalDamage = pureDamage;

            // NOTE: 크리티컬 확률, 크리티컬 데미지
            float isCritical = UnityEngine.Random.value; // NOTE: 0 <= isCritical <= 1
            if(isCritical < _attacker.criticalChance.finalValue)
                finalDamage *= (2.0f + Utilities.Max<float>(0, _attacker.criticalDamage.finalValue));

            // NOTE: 최종 데미지 증가
            finalDamage *= (1.0f + Utilities.Max<float>(0, _attacker.finalDamage.finalValue));

            return Utilities.Max<float>(1, finalDamage);
        }

        public static float GetFinalGroggy(Entity _publisher, Entity _receiver)
        {
            float finalMental = _receiver.mentality.finalValue;

            if(finalMental < 0.001f)
                return 0.001f;

            return finalMental;
        }
    }
}