using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class EntityMonster : EntityBase
    {
        [Header("AI Options")]
        public float waitSecondOnChangeAggro = 0.75f;
        public float waitDiffSecondOnChangeAggro = 0.25f;
        public float waitSecondOnChangeAction = 1.5f;
        public float waitDiffSecondOnChangeAction = 0.5f;

        [Header("Aggro Options")]
        public LTRB aggroRange;
        public EntitySensorGizmoOption aggroDebugOption;
        public bool bPrevAggro = false;
        public bool bAggro = false;
        public bool bUpdateAggroDirX = true;
        public bool bUpdateAggroDirY = false;
        public List<string> targetTags;
        public List<EntityBase> aggroTargets;
        public int targetLayerMask;

        public System.Random prng { get; private set; }

        protected override void Start()
        {
            base.Start();
            aggroTargets = new List<EntityBase>(4);
            prng = new System.Random();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public virtual void OnAggroBegin()
        {

        }

        public virtual void OnAggroEnd()
        {
            
        }
    }
}