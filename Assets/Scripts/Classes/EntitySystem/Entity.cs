using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(EntityController))]
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
        public Animator animator => m_animator;

        public float health => m_health;
        public float mana => m_mana;

        public VelocityModule2D vm { get; private set; }
        public System.Random prng { get; private set; }
#endregion

        [Header("Entity Identification")]
        public string entityName = "Unknown Entity";

        [Header("Entity Stat Table")]
        public Stat maxHealth;
        public Stat maxMana;

        public Stat strength;
        public Stat defence;
        public Stat fixedTakenDamage; // TODO: 고정 피해량 변수인데, 이 변수를 없에면서 같은 역할을 구현할 수 있는가?

        public Stat criticalChance;
        public Stat criticalDamage;
        public Stat finalDamage;

        public Stat gravityPercent;
        public Stat moveSpeedPercent;

        public Stat groggyStrength;
        public Stat maxGroggyValue;

        // TODO: 이 변수에 할당하는 동작을 자동화할 것.
        public List<Collider2D> volumeCollisions; // 지형 충돌 영역(= 물체의 부피)
        public List<Collider2D> battleTriggers; // 전투 트리거

        private Rigidbody2D m_physics;
        private SpriteRenderer m_spRenderer;
        private Animator m_animator;

        [Header("Entity Physical Variables")]
        public DirectionVector2 lookDir; // 시선 방향
        [HideInInspector] public Vector3 eulerRotation; // 방향 회전

        public int phase; // 엔티티 페이즈

        public bool bInvincibility; // 무적
        public float groggyValue; // TODO: groggyValue 이름 뭘로 바꿀지 고민해보기.

        // 애니메이션 클립 타이밍 체크 변수
        public bool bBeginOfAnimation;
        public bool bBeginOfAction;
        public bool bEndOfAction;
        public bool bEndOfAnimation;

        private float m_health = 1; // 현재 체력
        private float m_mana = 1; // 현재 마나

        [HideInInspector] public List<Collider2D> sensorBuffer;

#region 아직 정리 안 함.
        public bool bParrying;
        public Vector2 moveDir;
#endregion

#region Entity Events
        // MonoBehaviour.Awake()
        public virtual bool InitSingletonInstance() => true;

        public virtual void OnAwakeEntity()
        {
            TryGetComponent<Rigidbody2D>(out m_physics);
            TryGetComponent<SpriteRenderer>(out m_spRenderer);
            TryGetComponent<Animator>(out m_animator);

            vm = new VelocityModule2D(m_physics);
            prng = new System.Random();

            if(sensorBuffer == null)
                sensorBuffer = new List<Collider2D>();

            m_health = UnchordUtility.Max(1, maxHealth.finalValue);
        }

        // MonoBehaviour.OnEnable()
        public virtual void OnEnableEntity() {}

        // MonoBehaviour.Start()
        public virtual void OnStartEntity() {}
        public abstract IStateMachineBase InitStateMachine();
        public virtual bool InitActiveSelf() => true;

        // MonoBehaviour.FixedUpdate()

        // MonoBehaviour.Update()
        public virtual void OnEndOfEntity() {}

        // MonoBehaviour.LateUpdate()

        // MonoBehaviour.OnDrawGizmos()

        // MonoBehaviour.OnDisable()
        public virtual void OnDisableEntity() {}

#endregion

#region Entity Property Modifiers
        public float SetHealth(float _health)
        {
            m_health = UnchordUtility.Mid(0, maxHealth.finalValue, _health);
            return m_health;
        }

        public float ChangeHealth(float _dHealth)
        {
            return SetHealth(m_health + _dHealth);
        }

        public float SetMana(float _mana)
        {
            m_mana = UnchordUtility.Mid(0, maxMana.finalValue, _mana);
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
    }
}