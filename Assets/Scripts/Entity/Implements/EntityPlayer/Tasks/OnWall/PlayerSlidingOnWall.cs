using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerSlidingOnWall : PlayerOnWall
    {
        public Stat minSpeedStatY;
        public Stat gravityStatY;

        public PlayerSlidingOnWall(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            float ix = config.instance.axisInput.x;

            if(ix != 0.0f)
                return InvokeResult.Failure;

            config.instance.FixConstraints(false, false);
            float speedY = minSpeedStatY?.finalValue ?? -6.0f;
            float gravity = gravityStatY?.finalValue ?? -9.81f;

            float vy = config.instance.velModule.GetVelocityY() + gravity * Time.fixedDeltaTime;

            if(vy < speedY)
                vy = speedY;

            config.curTask = this;
            config.instance.velModule.SetVelocityXY(0.0f, vy);

            return InvokeResult.Success;
        }
    }
}