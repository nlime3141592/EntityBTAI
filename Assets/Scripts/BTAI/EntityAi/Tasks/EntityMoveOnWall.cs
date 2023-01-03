/*
namespace UnchordMetroidvania
{
    public class EntityMoveOnWall<T_Config> : EntityOnWall<T_Config>
    where T_Config : IEntityMovementOnWallConfig
    {
        public EntityMoveOnWall(T_Config config, int id, string name)
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

        }
    }
}
*/