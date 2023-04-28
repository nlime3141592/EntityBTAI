using UnityEngine;

namespace Unchord
{
    public abstract class PlayerState : EntityState<Player>
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.senseData.OnFixedUpdate(instance);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            instance.CURRENT_STATE = idConstant;
        }
    }
}