using UnityEngine;
namespace UnchordMetroidvania
{
    public class EntityFreeFallOnAir<T_Config> : EntityOnAir<T_Config>
    where T_Config : IEntityFreeFallConfig
    {
        public EntityFreeFallOnAir(T_Config config, int id, string name)
        : base(config, id, name)
        {

        }

        public override InvokeResult Invoke()
        {
            base.Invoke();
            p_Logic();
            return InvokeResult.SUCCESS;
        }

        protected virtual void p_Logic()
        {
            float xInput = p_config.xInput;
            float vx = 0.0f;

            if(xInput != 0)
                vx = p_config.baseSpeed * p_config.lookDirX;

            float vy = p_config.velModule.GetVelocityY();
            float speed = p_config.minFreeFallSpeed;
            float gravity = p_config.freeFallGravity;

            if(vy > speed)
                vy += (gravity * p_config.fixedDeltaTime);
            if(vy < speed)
                vy = speed;

            p_config.velModule.SetVelocityXY(vx, vy);
        }
    }
}