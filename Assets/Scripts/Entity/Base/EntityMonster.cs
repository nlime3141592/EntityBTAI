using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public abstract class EntityMonster : Entity
    {
        [Header("AI Options")]
        public float waitSecondOnChangeAggro = 0.75f;
        public float waitDiffSecondOnChangeAggro = 0.25f;
        public float waitSecondOnChangeAction = 1.5f;
        public float waitDiffSecondOnChangeAction = 0.5f;

        public MonsterAggroAI aggroAI;

        [Header("Aggro Options")]
        public LTRB aggroRange;
        public EntitySensorGizmoOption aggroDebugOption;
        public bool bPrevAggro = false;
        public bool bAggro = false;
        public bool bUpdateAggroDirX = true;
        public bool bUpdateAggroDirY = false;
        public List<string> targetTags;
        public List<Entity> aggroTargets;
        public int targetLayerMask;

        public int monsterPhase;

        protected override void InitComponents()
        {
            base.InitComponents();
        }

        protected override void InitMiscellaneous()
        {
            base.InitMiscellaneous();

            aggroTargets = new List<Entity>(4);

            aggroAI.onAggroBegin += OnAggroBegin;
            aggroAI.onAggroEnd += OnAggroEnd;
        }

        public virtual void OnAggroBegin() {}
        public virtual void OnAggroEnd() {}
    }
}