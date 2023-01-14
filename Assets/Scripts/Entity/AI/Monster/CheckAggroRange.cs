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
            Collider2D[] colTargets = EntitySensor.OverlapBox(
                instance,
                instance.aggroRange,
                instance.aggroDebugOption
            );

            instance.targets.FilterFromColliders(instance, colTargets, false, instance.targetTags.ToArray());
            instance.bAggro = instance.targets.Count > 0;

            float baseX = instance.transform.position.x;
            float targetX = baseX + 1;

            if(instance.targets != null && instance.targets.Count > 0)
                targetX = instance.targets[0].transform.position.x;

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