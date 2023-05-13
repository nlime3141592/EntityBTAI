using System;
using UnityEngine;

namespace Unchord
{
    [DisallowMultipleComponent]
    public class EntityController : EntityBehaviour
    {        
        public IStateMachineBase fsm { get; private set; }

        [Header("Debug Values")]
        public int CURRENT_STATE_ID_CONSTANT;
        public float CURRENT_HEALTH;

        protected override void Awake()
        {
            base.Awake();

            if(!entity.InitSingletonInstance())
            {
                Destroy(entity.gameObject);
                return;
            }

            entity.OnAwakeEntity();
        }

        protected override void Start()
        {
            base.Start();

            entity.OnStartEntity();
            fsm = entity.InitStateMachine();
            entity.gameObject.SetActive(entity.InitActiveSelf());
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
                entity.OnEndOfEntity();
                Destroy(entity.gameObject);
            }
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            fsm.LateUpdate();

            // Update Debug Values.
            CURRENT_STATE_ID_CONSTANT = fsm.state.idConstant;
            CURRENT_HEALTH = entity.health;
        }

        private bool m_bCanDestroy()
        {
            return entity.health <= 0 && !fsm.bStarted;
        }
    }
}