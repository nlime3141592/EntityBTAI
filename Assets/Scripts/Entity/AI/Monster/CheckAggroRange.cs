using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class CheckAggroRange<T> : ConditionNodeBT<T>
    where T : EntityMonster
    {
        public CheckAggroRange(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            // NOTE: 플레이어 탐지 동작을 이 곳에 넣기.
            EntityBase[] targets = EntityOverlapAI.GetEntities(
                config.instance,
                config.instance.aggroRange,
                false,
                1 << LayerMask.NameToLayer("Entity"),
                true,
                config.instance.targetTags.ToArray()
            );

            config.instance.bAggro = targets.Length > 0;
            config.instance.targets = targets;

            return InvokeResult.Success;
        }
    }
}