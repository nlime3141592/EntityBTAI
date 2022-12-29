namespace UnchordMetroidvania
{
    public class EntitySlidingOnWall<T_Config> : EntityMoveOnWall<T_Config>
    where T_Config : IEntitySlidingOnWallConfig
    {
        public EntitySlidingOnWall(T_Config config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override void p_Logic()
        {
            float vx = 0.0f;
            float vy = p_config.velModule.GetVelocityY();
            float speed = p_config.slidingSpeed;
            float gravity = p_config.slidingGravity;

            if(vy > speed)
                vy += (gravity * p_config.fixedDeltaTime);
            if(vy < speed)
                vy = speed;

            p_config.velModule.SetVelocityXY(vx, vy);
        }
    }
}