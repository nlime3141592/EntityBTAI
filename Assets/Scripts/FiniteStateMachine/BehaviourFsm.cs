using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class BehaviourFsm<T> : FiniteStateMachine<T>
    where T : MonoBehaviour
    {
        public BehaviourFsm(T _instance, int _capacity)
        : base(_instance, _capacity)
        {

        }
    }
}