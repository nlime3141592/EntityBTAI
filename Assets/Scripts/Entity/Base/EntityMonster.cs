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
        public LTRB aggroRange;
        public EntitySensorGizmo aggroDebugOption;
        public bool bPrevAggro = false;
        public bool bAggro = false;
        public List<string> targetTags;
        public List<EntityBase> targets;
        public InvokeResult animationResult = InvokeResult.Running;

        protected override void Start()
        {
            base.Start();

            targets = new List<EntityBase>(4);
        }

        public void PublishEndOfAnimation()
        {
            Debug.Log("End Animation.");
            animationResult = InvokeResult.Success;
        }

        protected override void p_Debug_OnPostInvoke()
        {
            base.p_Debug_OnPostInvoke();
        }

        public virtual void OnAggroBegin()
        {

        }

        public virtual void OnAggroEnd()
        {
            
        }
    }
}