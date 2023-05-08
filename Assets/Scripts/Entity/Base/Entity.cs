using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(AnimationController))]
    [DisallowMultipleComponent]
    public abstract class Entity : MonoBehaviour
    {
        // Entity, Non-Tile object.
        // 위치, 방향,
        // 속도, 질량, 부피,
        // 강도(체력으로 정의함)

#region Properties
        public Rigidbody2D physics => m_physics; // 물리 속성
        public SpriteRenderer spRendere => m_spRenderer; // 모양
        public AnimationController aController => m_aController; // 속성의 변경 제어
        public float health => m_health;
        public float mana => m_mana;

        public IStateMachineBase fsm { get; private set; }
        public VelocityModule2D vm { get; private set; }
        public System.Random prng { get; private set; }
#endregion

#region Allocate on Editor
        public List<Collider2D> volumeCollisions; // 지형 충돌 영역(= 물체의 부피)
        public List<Collider2D> battleTriggers; // 전투 트리거
        public List<Entity> subOrgans; // 개체의 하위 기관

        // base stats
        public Stat maxHealth; // 강도(체력)
        public Stat maxMana;

        // attack stats
        public Stat healthProportionDamage; // 체력 비례 데미지
        public Stat strength;
        public Stat criticalChance;
        public Stat criticalDamage;
        public Stat finalDamage;

        // defence stats
        public Stat fixedTakenDamage; // 고정 데미지
        public Stat defence;

        // other battle stats
        public Stat mentality;
#endregion

#region Components
        private Rigidbody2D m_physics;
        private SpriteRenderer m_spRenderer;
        private AnimationController m_aController;

#endregion

#region Variables
        public BoolVector2 bFixedLookDirByAxis; // 입력에 의한 시선 방향 전환 제어
        public DirectionVector2 lookDir; // 시선 방향
        public Vector3 eulerRotation; // 방향 회전

        public int phase;

        public bool bInvincibility;
        public float groggyValue;

        public List<Collider2D> sensorBuffer;

        private float m_health = 1;
        private float m_mana = 1;
#endregion

#region For Debugging
        public int CURRENT_STATE;
        public float CURRENT_HEALTH;
#endregion

#region 아직 정리 안 함.
        public bool bParrying;
        public Vector2 moveDir;
#endregion

#region Entity Events
        // MonoBehaviour.Awake()
        protected virtual bool InitSingletonInstance() => true;

        protected virtual void OnAwakeEntity()
        {
            TryGetComponent<Rigidbody2D>(out m_physics);
            TryGetComponent<SpriteRenderer>(out m_spRenderer);
            TryGetComponent<AnimationController>(out m_aController);

            vm = new VelocityModule2D(m_physics);
            prng = new System.Random();

            if(sensorBuffer == null)
                sensorBuffer = new List<Collider2D>();

            m_health = Utilities.Max<float>(1, maxHealth.finalValue);
        }

        protected virtual void OnStartEntity()
        {
            
        }

        // MonoBehaviour.Start()
        protected abstract IStateMachineBase InitStateMachine();

        // MonoBehaviour.FixedUpdate()

        // MonoBehaviour.Update()

        // MonoBehaviour.LateUpdate()

        // MonoBehaviour.OnDrawGizmos()

        // Extra.
        // protected virtual void OnZeroHealth() {}
        protected virtual void OnEndOfEntity() {}
/*
        private IEnumerator m_DestroyEntity()
        {
            // yield return new WaitUntil(() => m_bRegisteredMachineEvent);
            // yield return new WaitWhile(() => m_health <= 0);
            yield return new WaitUntil(() => bDeadState);

            void rec_SearchOrgan(Entity _organ)
            {
                int cntOrgan = _organ.subOrgans.Count;

                for(int i = 0; i < cntOrgan; ++i)
                    rec_SearchOrgan(_organ.subOrgans[i]);

                _organ.OnZeroHealth();
            }

            rec_SearchOrgan(this);
            yield return new WaitUntil(m_bCanDestroy);
            this.OnEndOfEntity();
        }
*/
        private bool m_bCanDestroy()
        {
            return m_health <= 0 && !fsm.bStarted;
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

        public void IgnoreBattleTrigger(Collider2D _target, bool _bIgnore) => m_IgnoreCollision(battleTriggers, _target, _bIgnore);
        public void IgnoreVolumeCollision(Collider2D _target, bool _bIgnore) => m_IgnoreCollision(volumeCollisions, _target, _bIgnore);

        private void m_IgnoreCollision(List<Collider2D> _baseCollection, Collider2D _target, bool _bIgnore)
        {
            if(_target == null)
                return;

            int count = _baseCollection.Count;

            for(int i = 0; i < count; ++i)
                if(_baseCollection[i] != null)
                    Physics2D.IgnoreCollision(_baseCollection[i], _target, _bIgnore);
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

            OnAwakeEntity();
        }

        private void Start()
        {
            OnStartEntity();
            fsm = InitStateMachine();
        }

        private void FixedUpdate()
        {
            fsm.FixedUpdate();
        }

        private void Update()
        {
            fsm.Update();

            if(m_bCanDestroy())
            {
                OnEndOfEntity();
                Destroy(this.gameObject);
            }
        }

        private void LateUpdate()
        {
            fsm.LateUpdate();
        }
#endregion
    }
}