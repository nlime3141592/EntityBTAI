/*
using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _EntityActionTask : _BehaviourTask<_EntityBase>
    {
        public int id { get; private set; }
        public string name { get; private set; }

        protected _EntityActionTask(_EntityBase entity, int id, string name)
        : base(entity)
        {
            this.id = id;
            this.name = name;
        }

        public abstract void OnInvoke();

        public virtual void OnTaskChanged()
        {

        }
    }
}
*/