/*
namespace UnchordMetroidvania
{
    public abstract class EntityOnFloor<T_Config> : TaskNodeBT<T_Config>
    where T_Config : IEntityMovementConfig
    {
        protected ITerrainCheckerConfig p_floorCheckerConfig { get; private set; }

        protected EntityOnFloor(T_Config config, ITerrainCheckerConfig floorCheckerConfig, int id, string name)
        : base(config, id, name)
        {
            p_floorCheckerConfig = floorCheckerConfig;
        }

        public override InvokeResult Invoke()
        {
            base.Invoke();
            p_config.currentState = this.id;
            return InvokeResult.SUCCESS;
        }
    }
}
*/