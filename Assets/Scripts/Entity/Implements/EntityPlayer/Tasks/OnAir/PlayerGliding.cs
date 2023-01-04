using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerGliding : PlayerAction
    {
        public Stat speedStatX;
        public Stat speedStatY;
        public Stat gravityStatY;

        public PlayerGliding(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.axisInput.y <= 0.0f)
                return InvokeResult.Failure;

            float ix = config.instance.axisInput.x;

            float speedX = speedStatX?.finalValue ?? 1.0f;
            float speedY = speedStatY?.finalValue ?? -3.0f;
            float gravity = gravityStatY?.finalValue ?? 9.81f;

            float vx = speedX * ix;
            float vy = config.instance.velModule.GetVelocityY();

            float dY = vy - speedY;
            float dir = 0.0f;

            if(dY != 0.0f)
                dir = (vy - speedY) / Math.Abs(vy - speedY);

            vy -= dir * gravity;

            if(vy != speedY)
            {
                if(dir < 0 && vy < speedY)
                    vy = speedY;
                else if(dir > 0 && vy > speedY)
                    vy = speedY;
            }

            config.curTask = this;
            config.instance.FixConstraints(false, false);
            config.instance.velModule.SetVelocityXY(vx, vy);

            return InvokeResult.Success;
        }
    }
}