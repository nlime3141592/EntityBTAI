/*
namespace UnchordMetroidvania
{
    public class _EntityMoveOnFloorTask : _EntityOnFloorTask
    {
        public float baseSpeed;
        public float appliedSpeed;

        public _EntityMoveOnFloorTask(_EntityBase entity, int id, string name)
        : base(entity, id, name)
        {
            
        }

        public override void OnInvoke()
        {
            appliedSpeed = baseSpeed;

            float vx = behaviour.moveDir.x * appliedSpeed;
            float vy = behaviour.moveDir.y * appliedSpeed;
            behaviour.velModule.SetVelocityXY(vx, vy);
        }
    }
}
*/