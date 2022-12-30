using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _EntityOnLedgeTask : _EntityActionTask
    {
        protected _EntityOnLedgeTask(_EntityBase entity, int id, string name)
        : base(entity, id, name)
        {

        }
    }
}