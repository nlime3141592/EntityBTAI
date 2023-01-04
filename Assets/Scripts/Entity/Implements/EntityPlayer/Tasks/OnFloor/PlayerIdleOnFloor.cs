using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerIdleOnFloor : PlayerOnFloor
    {
        public PlayerIdleOnFloor(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            config.instance.FixConstraints(true, false);
            config.instance.velModule.SetVelocityXY(0.0f, -1.0f);
            return InvokeResult.Running;
        }
    }
}