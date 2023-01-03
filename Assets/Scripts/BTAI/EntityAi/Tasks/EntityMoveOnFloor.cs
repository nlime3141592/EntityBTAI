/*
using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityMoveOnFloor<T_Config> : EntityOnFloor<T_Config>
    where T_Config : IEntityMovementConfig
    {
        public float speedWeight { get; set; } = 1.0f;

        public EntityMoveOnFloor(T_Config config, ITerrainCheckerConfig floorCheckerConfig, int id, string name)
        : base(config, floorCheckerConfig, id, name)
        {

        }

        public override InvokeResult Invoke()
        {
            base.Invoke();

            Vector2 normal = p_floorCheckerConfig.terrain.normal;
            float dx = 1.0f;
            float dy = -normal.x / normal.y;
            p_config.moveDir = new Vector2(dx, dy);

            int dir = p_config.lookDirX;
            float speed = p_config.baseSpeed * speedWeight;
            float vx = dx * dir * speed;
            float vy = dy * dir * speed;

            p_config.velModule.SetVelocityXY(vx, vy);
            return InvokeResult.SUCCESS;
        }
    }
}
*/