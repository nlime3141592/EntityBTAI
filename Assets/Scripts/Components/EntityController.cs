using System;
using UnityEngine;

namespace Unchord
{
    [DisallowMultipleComponent]
    public class EntityController : ExtendedComponent<Entity>
    {        
        public IStateMachineBase fsm { get; private set; }

        [Header("Debug Values")]
        public int CURRENT_STATE_ID_CONSTANT;
        public float CURRENT_HEALTH;

        protected override void Awake()
        {
            base.Awake();

            if(!baseComponent.InitSingletonInstance())
            {
                Destroy(baseComponent.gameObject);
                return;
            }

            baseComponent.OnAwakeEntity();
        }

        protected override void Start()
        {
            base.Start();

            baseComponent.OnStartEntity();
            fsm = baseComponent.InitStateMachine();
            baseComponent.gameObject.SetActive(baseComponent.InitActiveSelf());
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            fsm.FixedUpdate();
        }

        protected override void Update()
        {
            base.Update();

            fsm.Update();

            if(m_bCanDestroy())
            {
                baseComponent.OnEndOfEntity();
                Destroy(baseComponent.gameObject);
            }
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            fsm.LateUpdate();

            // Update Debug Values.
            CURRENT_STATE_ID_CONSTANT = fsm.state.idConstant;
            CURRENT_HEALTH = baseComponent.health;
        }

        private bool m_bCanDestroy()
        {
            return baseComponent.health <= 0 && !fsm.bStarted;
        }
    }
}