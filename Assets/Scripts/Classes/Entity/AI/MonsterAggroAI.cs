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

        [HideInInspector] public Entity entity;

        public AreaSensorBox sensor;
        public List<Entity> targets;
        public List<string> tags;
        public LayerMask mask;

        public bool bIgnoreSelf = true;
        public bool bCanAggro = true;
        public bool bAggro;
        [NonSerialized] private bool bAggroPrev;

        [NonSerialized] private List<Collider2D> m_sensed;
        [NonSerialized] private List<Entity> m_tmp_targets;

        public MonsterAggroAI()
        {
            m_sensed = new List<Collider2D>(1);
            m_tmp_targets = new List<Entity>(1);
        }

        public void OnFixedUpdate()
        {
            sensor.transform.lpx = entity.transform.position.x;
            sensor.transform.lpy = entity.transform.position.y;
            sensor.transform.lfx = entity.transform.eulerAngles.y == 180;
            sensor.transform.lfy = entity.transform.eulerAngles.x == 180;
            sensor.OnUpdate();

            m_tmp_targets.Clear();
            m_sensed.Clear();

            if(bCanAggro)
            {
                sensor.Sense(m_sensed, tags, mask);

                if(bIgnoreSelf)
                    m_sensed.RemoveAll((collider) => entity.volumeCollisions.Contains(collider) || entity.battleTriggers.Contains(collider));

                m_sensed.GetComponents<Entity>(in m_tmp_targets);

                targets.RemoveAll((entity) => !m_tmp_targets.Contains(entity));

                for(int i = 0; i < m_tmp_targets.Count; ++i)
                    if(!targets.Contains(m_tmp_targets[i]))
                        targets.Add(m_tmp_targets[i]);
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