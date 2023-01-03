/*
namespace UnchordMetroidvania
{
    public class EntityIdleOnFloor<T_Config> : EntityOnFloor<T_Config>
    where T_Config : IEntityMovementConfig
    {
        public EntityIdleOnFloor(T_Config config, ITerrainCheckerConfig floorCheckerConfig, int id, string name)
        : base(config, floorCheckerConfig, id, name)
        {

        }

        public override InvokeResult Invoke()
        {
            base.Invoke();

            p_config.velModule.SetVelocityXY(0.0f, 0.0f);
            return InvokeResult.RUNNING;
        }
    }
}
*/