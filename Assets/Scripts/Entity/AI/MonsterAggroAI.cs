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

        public Sensor_SO aggroSensor;
        public LayerMask targetLayer;
        public List<string> targetTags;

        [HideInInspector] public List<Entity> aggroTargets;
        [HideInInspector] public List<Entity> aggroTargetsPrev;
        [HideInInspector] public bool bAggroPrev = false;
        [HideInInspector] public bool bAggro = false;
        private List<Collider2D> m_detected;

        public MonsterAggroAI()
        {
            if(m_detected == null)
                m_detected = new List<Collider2D>();
        }

        public void OnFixedUpdate()
        {
            m_DetectTargets();
            m_PublishEvents();
        }

        private void m_DetectTargets()
        {
            if(aggroSensor == null)
                return;

            List<Entity> tmp_aggros = aggroTargets;
            aggroTargets = aggroTargetsPrev;
            aggroTargetsPrev = tmp_aggros;

            bAggroPrev = bAggro;
            aggroTargets.Clear();
            aggroSensor.Sense(in m_detected, targetLayer);

            for(int i = 0; i < m_detected.Count; ++i)
            {
                Entity dEntity;

                if(m_detected[i].gameObject.TryGetComponent(out dEntity))
                    if(!aggroTargets.Contains(dEntity))
                        aggroTargets.Add(dEntity);
            }

            bAggro = aggroTargets.Count > 0;
        }

        private void m_PublishEvents()
        {
            if(bAggro && !bAggroPrev)
                onAggroBegin?.Invoke();
            else if(bAggroPrev)
                onAggroEnd?.Invoke();
        }
    }
}