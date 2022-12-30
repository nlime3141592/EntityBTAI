using UnityEngine;

namespace UnchordMetroidvania
{
    public class _EntityAccelMoveOnFloorTask : _EntityMoveOnFloorTask
    {
        public int baseFrame;

        public int appliedFrame;
        public int executedFrame;

        public _EntityAccelMoveOnFloorTask(_EntityBase entity, int id, string name)
        : base(entity, id, name)
        {
            
        }

        public override void OnInvoke()
        {
            appliedFrame = baseFrame;
        }

        public override void OnTaskChanged()
        {
            executedFrame = -1;
        }
    }
}