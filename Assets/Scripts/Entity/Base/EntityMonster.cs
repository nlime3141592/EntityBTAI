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

        public MonsterAggroAI aggroAi;

        // TODO: 사마귀에 포함시키기.
        public int frame_idleTimeMin = 60;
        public int frame_idleTimeMax = 120;
        public int frame_idleAggroDelay = 70;

        protected override void OnAwakeEntity()
        {
            base.OnAwakeEntity();
        }

        protected override void OnStartEntity()
        {
            base.OnStartEntity();
        }
    }
}