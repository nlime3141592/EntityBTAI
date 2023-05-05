using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public class MonsterAggroAI
    {
        public event Action onAggroBegin;
        public event Action onAggroEnd;

        public AreaSensorBox sensor;
        public List<Entity> targets;
        [HideInInspector] public List<Entity> targetsPrev;
        public List<string> tags;
        public LayerMask mask;

        public bool bCanAggro = true;
        public bool bAggroPrev;
        public bool bAggro;

        [NonSerialized] private List<Collider2D> m_sensed;

        public MonsterAggroAI()
        {
            m_sensed = new List<Collider2D>(1);
        }

        public void OnFixedUpdate()
        {
            targetsPrev.Clear();
            List<Entity> tmp_aggros = targets;
            targets = targetsPrev;
            targetsPrev = tmp_aggros;

            if(bCanAggro)
            {
                sensor.Sense(m_sensed, tags, mask);
                m_sensed.GetComponents<Entity>(in targets);
            }

            bAggroPrev = bAggro;
            bAggro = targets.Count > 0;

            if(bAggro && !bAggroPrev)
                onAggroBegin?.Invoke();
            else if(bAggroPrev)
                onAggroEnd?.Invoke();
        }
    }
}