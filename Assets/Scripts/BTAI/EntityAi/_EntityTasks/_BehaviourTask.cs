using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _BehaviourTask<T>
    where T : MonoBehaviour
    {
        public T behaviour { get; private set; }

        protected _BehaviourTask(T behaviour)
        {
            this.behaviour = behaviour;
        }
    }
}