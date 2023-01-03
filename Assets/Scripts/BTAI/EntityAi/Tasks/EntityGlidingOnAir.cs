/*
namespace UnchordMetroidvania
{
    public class EntityGlidingOnAir<T_Config> : EntityFreeFallOnAir<T_Config>
    where T_Config : IEntityGlidingConfig
    {
        public EntityGlidingOnAir(T_Config config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override void p_Logic()
        {
            float xInput = p_config.xInput;
            float vx = 0.0f;

            if(xInput != 0)
                vx = p_config.baseSpeed * p_config.lookDirX;

            float vy = p_config.velModule.GetVelocityY();
            float speed = p_config.glidingSpeed;
            float accel = p_config.glidingAcceleration;

            if(vy > speed)
            {
                vy -= (accel * p_config.fixedDeltaTime);
                if(vy < speed) vy = speed;
            }
            else if(vy < speed)
            {
                vy += (accel * p_config.fixedDeltaTime);
                if(vy > speed) vy = speed;
            }

            p_config.velModule.SetVelocityXY(vx, vy);
        }
    }
}
*/