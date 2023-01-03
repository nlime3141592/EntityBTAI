/*
namespace UnchordMetroidvania
{
    public class EntityIdleOnAir<T_Config> : EntityOnAir<T_Config>
    where T_Config : IEntityMovementConfig
    {
        public EntityIdleOnAir(T_Config config, int id, string name)
        : base(config, id, name)
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