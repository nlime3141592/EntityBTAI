using UnityEngine;

namespace UnchordMetroidvania
{
    public interface IEntityMovementConfig : IConfigurationBT, IEntityLookConfig
    {
        IEntityMovementConfig movementConfig { get; }

        Vector2 moveDir { get; set; }
        Rigidbody2D physics { get; set; }
        VelocityController2D velModule { get; set; }

        float baseSpeed { get; set; }
        float gravity { get; set; }
    }
}