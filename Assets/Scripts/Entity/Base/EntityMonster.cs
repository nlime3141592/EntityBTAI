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

        // TODO: 사마귀에 포함시키기.
        public int frame_idleTimeMin = 60;
        public int frame_idleTimeMax = 120;
        public int frame_idleAggroDelay = 70;

        public int monsterPhase; // TODO: Entity.phase 변수를 사용할 예정이므로 EntityMonster.monsterPhase 변수 지우기.

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