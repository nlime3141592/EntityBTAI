using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _EntityOnCeilTask : _EntityActionTask
    {
        protected _EntityOnCeilTask(_EntityBase entity, int id, string name)
        : base(entity, id, name)
        {

        }
    }
}