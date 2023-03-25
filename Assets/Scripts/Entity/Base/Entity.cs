using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnchordMetroidvania;

namespace Unchord
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(AnimationController))]
    public abstract class Entity : MonoBehaviour
    {
        // Entity, Non-Tile object.
        // 위치, 방향,
        // 속도, 질량, 부피,
        // 강도(체력으로 정의함)

        public Rigidbody2D physics => m_physics; // 물리 속성
        public SpriteRenderer spRendere => m_spRenderer; // 모양
        public AnimationController aController => m_aController; // 속성의 변경 제어
        public List<Collider2D> volumeColliders; // 부피
        public List<Entity> subOrgans; // 개체의 하위 기관
        public DirectionVector2 lookDir; // 방향
        public BoolVector2 bFixLookDir; // 방향 전환 제어
        public Vector3 eulerRotation;
        public Stat maxHealth; // 강도(체력)
        public Stat maxMana;
        public float health => m_health;
        public float mana => m_mana;

        private Rigidbody2D m_physics;
        private SpriteRenderer m_spRenderer;
        private AnimationController m_aController;
        private float m_health;
        private float m_mana;
        public bool bEndOfEntity;

        private bool m_bRegisteredMachineEvent;
        public IStateMachineRemote machineInterface;

#region 아직 정리 안 함.
        public VelocityModule2D vm;
        public bool bParrying;
#endregion

#region Entity Events
        // MonoBehaviour.Awake()
        protected virtual bool InitSingletonInstance() => true;

        protected virtual void InitComponents()
        {
            TryGetComponent<Rigidbody2D>(out m_physics);
            TryGetComponent<SpriteRenderer>(out m_spRenderer);
            TryGetComponent<AnimationController>(out m_aController);
        }

        protected virtual void InitStateMachine() {}

        protected virtual void InitMiscellaneous()
        {
            bEndOfEntity = false;
            m_health = Utilities.Max<float>(1, maxHealth.finalValue);
        }

        // MonoBehaviour.Start()

        // MonoBehaviour.FixedUpdate()
        protected virtual void PreFixedUpdate() {}
        protected virtual void PostFixedUpdate() {}

        // MonoBehaviour.Update()
        protected virtual void PreUpdate() {}
        protected virtual void PostUpdate() {}

        // MonoBehaviour.LateUpdate()

        // MonoBehaviour.OnDrawGizmos()
        protected virtual bool bShowGizmos() => false;

        // Extra.
        protected virtual void OnZeroHealth() {}
        protected virtual void OnEndOfEntity() {}

        private IEnumerator m_DestroyEntity()
        {
            yield return new WaitUntil(() => m_bRegisteredMachineEvent);
            yield return new WaitWhile(() => m_health <= 0);

            void rec_SearchOrgan(Entity _organ)
            {
                int cntOrgan = _organ.subOrgans.Count;

                for(int i = 0; i < cntOrgan; ++i)
                    rec_SearchOrgan(_organ.subOrgans[i]);

                _organ.OnZeroHealth();
            }

            rec_SearchOrgan(this);

            yield return new WaitUntil(() => bEndOfEntity);
            this.OnEndOfEntity();
        }
#endregion

#region Entity Property Modifiers
        public float SetHealth(float _health)
        {
            m_health = Utilities.Mid<float>(0, maxHealth.finalValue, _health);
            return m_health;
        }

        public float ChangeHealth(float _dHealth)
        {
            return SetHealth(m_health + _dHealth);
        }

        public float SetMana(float _mana)
        {
            m_mana = Utilities.Mid<float>(0, maxMana.finalValue, _mana);
            return m_mana;
        }

        public float ChangeMana(float _dMana)
        {
            return SetMana(m_mana + _dMana);
        }

        protected void RegisterMachineEvent(IStateMachineRemote _machineInterface)
        {
            if(m_bRegisteredMachineEvent)
                return;
            else if(_machineInterface == null)
                return;

            m_bRegisteredMachineEvent = true;
            machineInterface = _machineInterface;
        }

        protected void UnregisterMachineEvent()
        {
            if(!m_bRegisteredMachineEvent)
                return;

            m_bRegisteredMachineEvent = false;
            machineInterface = null;
        }
#endregion

#region Unity Event Functions
        private void Awake()
        {
            if(!InitSingletonInstance())
            {
                Destroy(this.gameObject);
                return;
            }

            InitComponents();
            InitStateMachine();
            InitMiscellaneous();
        }

        private void Start()
        {
            StartCoroutine(m_DestroyEntity());
        }

        private void FixedUpdate()
        {
            if(!m_bRegisteredMachineEvent)
                return;

            PreUpdate();
            machineInterface.FixedUpdate();
            PostUpdate();
        }

        private void Update()
        {
            if(!m_bRegisteredMachineEvent)
                return;

            PreUpdate();
            machineInterface.Update();
            PostUpdate();
        }

        private void LateUpdate()
        {
            if(!m_bRegisteredMachineEvent)
                return;

            machineInterface.LateUpdate();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(!m_bRegisteredMachineEvent)
                return;

            machineInterface.OnDrawGizmos(bShowGizmos());
        }
#endif
#endregion
    }
}