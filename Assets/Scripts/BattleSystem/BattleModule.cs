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
            float finalStrength = owner.strength.finalValue;
            float finalDefence = target.defence.finalValue;
            float finalDamage = finalStrength - finalDefence;

            if(finalDamage < 1)
                finalDamage = 1;

            target.SetHealth(target.health - finalDamage);
        }
    }
}