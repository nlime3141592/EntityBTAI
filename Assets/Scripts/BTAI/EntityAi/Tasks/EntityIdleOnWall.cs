/*
namespace UnchordMetroidvania
{
    public class EntityIdleOnWall<T_Config> : EntityOnWall<T_Config>
    where T_Config : IEntityOnWallConfig
    {
        public EntityIdleOnWall(T_Config config, int id, string name)
        : base(config, id, name)
        {

        }

        public override InvokeResult Invoke()
        {
            base.Invoke();

            p_config.velModule.SetVelocityXY(0.0f, 0.0f);
            return InvokeResult.SUCCESS;
        }
    }
}
*/