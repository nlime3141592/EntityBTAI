using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerIdleOnFloor : PlayerOnFloor
    {
        public PlayerIdleOnFloor(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override void p_OnPreInvokeNode()
        {
            config.instance.physics.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }

        protected override InvokeResult p_Invoke()
        {
            config.instance.velModule.SetVelocityXY(0.0f, -0.1f);
            return InvokeResult.Running;
        }
    }
}