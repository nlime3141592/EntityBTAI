using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UnchordMetroidvania
{
    [Serializable]
    public class Stat
    {
        public float finalValue
        {
            get
            {
                if(m_bShouldUpdate || baseValue != m_baseValue)
                {
                    m_bShouldUpdate = false;
                    m_baseValue = baseValue;
                    m_finalValue = m_CalculateFinalValue();
                }

                return m_finalValue;
            }
        }
        private float m_finalValue;

        public float baseValue;
        private float m_baseValue = float.MinValue;

        private bool m_bShouldUpdate = true;

        public ReadOnlyCollection<StatModifier> modifiers;
        private List<StatModifier> m_modifiers;

        public Stat()
        : this(0, 4)
        {

        }

        public Stat(float baseValue, int capacity = 4)
        {
            this.baseValue = baseValue;

            m_modifiers = new List<StatModifier>(capacity < 0 ? 4 : capacity);
            modifiers = m_modifiers.AsReadOnly();
        }

        public void AddModifier(StatModifier mod)
        {
            // if(m_modifiers.Contains(mod)) return;
            m_bShouldUpdate = true;
            m_modifiers.Add(mod);
            m_modifiers.Sort(m_CompareModifiers);
        }

        public bool RemoveModifier(StatModifier mod)
        {
            if(m_modifiers.Remove(mod))
            {
                m_bShouldUpdate = true;
                return true;
            }

            return false;
        }

        public bool RemoveAllModifiersFromSource(object source)
        {
            bool bRemoved = false;

            for(int i = m_modifiers.Count - 1; i >= 0; --i)
            {
                if(m_modifiers[i].source == source)
                {
                    m_bShouldUpdate =true;
                    bRemoved = true;
                    m_modifiers.RemoveAt(i);
                }
            }

            return bRemoved;
        }

        private int m_CompareModifiers(StatModifier a, StatModifier b)
        {
            if(a.order < b.order)
                return -1;
            else if(a.order > b.order)
                return 1;
            else
                return 0;
        }

        private float m_CalculateFinalValue()
        {
            float final = baseValue;
            float addedPercent = 0;

            for(int i = 0; i < m_modifiers.Count; ++i)
            {
                StatModifier mod = m_modifiers[i];

                if(mod.type == StatModType.Flat)
                {
                    final += mod.value;
                }
                else if(mod.type == StatModType.PercentAdd)
                {
                    addedPercent += mod.value;

                    if(i + 1 == m_modifiers.Count || m_modifiers[i + 1].type != StatModType.PercentAdd)
                    {
                        final *= (1 + addedPercent);
                        addedPercent = 0;
                    }
                }
                else if(mod.type == StatModType.PercentMul)
                {
                    final *= (1 + mod.value);
                }
            }

            return (float)Math.Round(final, 4);
        }
    }
}