using UnityEngine;

namespace Unchord
{
    public abstract class StateEventTrigger : MonoBehaviour
    {
        protected Entity entity => m_entity;
        private Entity m_entity;

        private void OnValidate()
        {
            TryGetComponent<Entity>(out m_entity);
        }

        private void Awake()
        {
            TryGetComponent<Entity>(out m_entity);
        }
    }
}