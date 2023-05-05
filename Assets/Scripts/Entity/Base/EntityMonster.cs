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
        public List<string> targetTags;
        public LayerMask targetLayerMask;
        public List<Entity> aggroTargets; // TODO: 빌드 시에 HideInInspector Attribute를 달아줘야 함.

        // TODO: 사마귀에 포함시키기.
        public int frame_idleTimeMin = 60;
        public int frame_idleTimeMax = 120;
        public int frame_idleAggroDelay = 70;

        public bool bAggroPrev;
        public bool bAggro;

        protected override void OnAwakeEntity()
        {
            base.OnAwakeEntity();

            aggroTargets = new List<Entity>(4);
        }

        protected override void OnStartEntity()
        {
            base.OnStartEntity();

            aggroAI.onAggroBegin += OnAggroBegin;
            aggroAI.onAggroEnd += OnAggroEnd;
        }

        public virtual void OnAggroBegin() {}
        public virtual void OnAggroEnd() {}
    }
}