using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class CheckAggroRange<T> : TaskNodeBT<T>
    where T : EntityMonster
    {
        public CheckAggroRange(T instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            // NOTE: 플레이어 탐지 동작을 이 곳에 넣기.
            EntityBase[] targets = EntityOverlapAI.GetEntities(
                instance,
                instance.aggroRange,
                false,
                1 << LayerMask.NameToLayer("Entity"),
                true,
                instance.targetTags.ToArray()
            );

            instance.bAggro = targets.Length > 0;
            instance.targets = targets;

            float baseX = instance.transform.position.x;
            float targetX = baseX + 1;

            if(targets != null && targets.Length > 0)
                targetX = targets[0].transform.position.x;

            float dX = targetX - baseX;

            if(!instance.bFixLookDirX)
                if(dX < 0)
                    instance.lookDir.x = -1;
                else if(dX >= 0)
                    instance.lookDir.x = 1;

            return InvokeResult.Success;
        }
    }
}