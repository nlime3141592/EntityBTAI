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
        public int CURRENT_PHASE;
        public float CURRENT_HEALTH;
        public float CURRENT_GROGGY_VALUE;

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

        protected override void OnEnable()
        {
            base.OnEnable();

            baseComponent.OnEnableEntity();
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
            CURRENT_PHASE = baseComponent.phase;
            CURRENT_HEALTH = baseComponent.health;
            CURRENT_GROGGY_VALUE = baseComponent.groggyValue;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            baseComponent.OnDisableEntity();
        }

        private bool m_bCanDestroy()
        {
            return baseComponent.health <= 0 && !fsm.bStarted;
        }
    }
}