/*
using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityIsRun<EntityBase> : ConditionNodeBT<EntityBase>
    {
        public EntityIsRun(ConfigurationBT<EntityBase> config, int id, string name)
        : base(config, -1, "EntityIsRun")
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.bIsRun)
                return InvokeResult.Success;
            else
                return InvokeResult.Failure;
        }
    }
}
*/