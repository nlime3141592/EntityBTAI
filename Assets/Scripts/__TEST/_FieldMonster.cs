using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class _FieldMonster : Entity
    {
        public const int c_st_IDLE = 0;
        public const int c_st_WALK = 1;
        public const int c_st_FUNC_NON_AGGRESSIVE = 2;
        public const int c_st_FUNC_AGGRESSIVE = 3;

        public bool bAggro;
        public List<Entity> aggroTargets;

        public ReservoirSampler reservoir { get; private set; }
        public int nextTransit = -1;

        [Header("Non-Aggressive State Transit Weight")]
        public List<int> weight0;

        [Header("Aggressive State Transit Weight")]
        public List<int> weight1;

        public override void OnAwakeEntity()
        {
            base.OnAwakeEntity();

            reservoir = new ReservoirSampler();
        }

        public override IStateMachineBase InitStateMachine()
        {
            return null;
        }
    }
}