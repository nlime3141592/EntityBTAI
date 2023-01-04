using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerFreeFall : PlayerAction
    {
        public Stat speedStatX;
        public Stat minSpeedStatY;
        public Stat gravityStatY;

        public PlayerFreeFall(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            float ix = config.instance.axisInput.x;

            float speedX = speedStatX?.finalValue ?? 1.0f;
            float speedY = minSpeedStatY?.finalValue ?? -12.0f;
            float gravity = gravityStatY?.finalValue ?? -9.81f;

            float vx = speedX * ix;
            float vy = config.instance.velModule.GetVelocityY() + gravity * Time.fixedDeltaTime;

            if(vy < speedY)
                vy = speedY;
            // Debug.Log(string.Format($"{ix}, {speedX}, {vx}"));

            config.curTask = this;
            config.instance.FixConstraints(false, false);
            config.instance.velModule.SetVelocityXY(vx, vy);

            return InvokeResult.Success;
        }
    }
}