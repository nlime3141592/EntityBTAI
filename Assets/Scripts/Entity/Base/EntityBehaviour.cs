using System;
using UnityEngine;

namespace Unchord
{
    public abstract class EntityBehaviour : MonoBehaviour
    {
        public Entity entity => m_entity;
        private Entity m_entity;

        protected virtual void OnValidate()
        {
            TryGetComponent<Entity>(out m_entity);
        }

        protected virtual void Awake()
        {
            TryGetComponent<Entity>(out m_entity);
        }

        protected virtual void Start() {}
        protected virtual void FixedUpdate() {}
        protected virtual void Update() {}
        protected virtual void LateUpdate() {}
    }
}