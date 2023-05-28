using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Handler/Entity Aggression Events (SEH)")]
    public sealed class SEH_EntityAggression : StateEventHandler<IEntityAggressionEvents>
    {
        public AreaSensorBox boxSensor;
        public List<Entity> targets;
        public List<string> tags;
        public LayerMask mask;

        public bool bIgnoreSelf = true;
        public bool bCanAggro = true;

        public bool bAggro;
        public bool bAggroPrev;

        private List<Collider2D> m_sensed;
        private List<Entity> m_tmp_targets;

        protected override void Awake()
        {
            base.Awake();

            if(m_sensed == null)
                m_sensed = new List<Collider2D>(4);
            
            if(m_tmp_targets == null)
                m_tmp_targets = new List<Entity>(1);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            UpdateEventListener();

            Entity entity = baseComponent.baseComponent;
            boxSensor.transform.lpx = entity.transform.position.x;
            boxSensor.transform.lpy = entity.transform.position.y;
            boxSensor.transform.lfx = entity.transform.eulerAngles.y == 180;
            boxSensor.transform.lfy = entity.transform.eulerAngles.x == 180;
            boxSensor.OnUpdate();

            m_tmp_targets.Clear();
            m_sensed.Clear();

            if(bCanAggro)
            {
                boxSensor.Sense(m_sensed, tags, mask);

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

            if(!bAggro)
            {
                if(bAggroPrev)
                    iEvListener?.OnAggroEnd(this);
            }
            else
            {
                if(!bAggroPrev)
                    iEvListener?.OnAggroBegin(this);

                iEvListener?.OnAggressive(this);
            }
        }
    }
}