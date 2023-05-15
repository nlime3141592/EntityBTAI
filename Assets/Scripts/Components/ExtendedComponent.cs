using UnityEngine;

namespace Unchord
{
    public abstract class ExtendedComponent<T_Component> : MonoBehaviour
    where T_Component : UnityEngine.Component
    {
        public T_Component baseComponent => m_baseComponent;
        private T_Component m_baseComponent;

        protected virtual void OnValidate()
        {
            TryGetComponent<T_Component>(out m_baseComponent);
        }

        protected virtual void Awake()
        {
            TryGetComponent<T_Component>(out m_baseComponent);
        }

        protected virtual void Start()
        {

        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void LateUpdate()
        {
            
        }
    }
}