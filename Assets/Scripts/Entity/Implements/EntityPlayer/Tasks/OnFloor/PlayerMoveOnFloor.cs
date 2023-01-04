using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerMoveOnFloor : PlayerOnFloor
    {
        public Stat speedStat;

        public PlayerMoveOnFloor(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override void p_OnPreInvokeNode()
        {
            config.instance.physics.constraints &= ~(RigidbodyConstraints2D.FreezePositionX);
        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.axisInput.x != 0)
            {
                float speed = speedStat?.finalValue ?? 1.0f;
                Vector2 moveDir = config.instance.moveDir;
                float dx = speed * moveDir.x * config.instance.axisInput.x;
                float dy = speed * moveDir.y * config.instance.axisInput.x;

                config.curTask = this;
                config.instance.velModule.SetVelocityXY(dx, dy);
                return InvokeResult.Running;
            }
            else
            {
                return InvokeResult.Failure;
            }
        }
    }
}