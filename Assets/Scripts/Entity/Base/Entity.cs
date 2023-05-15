using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(SET_AnimationEvents))]
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

#region Stat Table
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

        // 생각...
        // Stat이 나타내는 값이, 어떤 지표의 상한선(최대값 또는 최소값)인 경우에, 새로운 필드 변수를 지정해야 됨.
        // Stat이 나타내는 값을 그대로 활용해야 한다면, Stat Modifier를 추가해서 finalValue를 얻어내는 방식을 사용함.

#endregion

#region Allocate on Editor
        public List<Collider2D> volumeCollisions; // 지형 충돌 영역(= 물체의 부피)
        public List<Collider2D> battleTriggers; // 전투 트리거

#endregion

#region Components
        private Rigidbody2D m_physics;
        private SpriteRenderer m_spRenderer;
        private Animator m_animator;
#endregion

#region Variables
        public DirectionVector2 lookDir; // 시선 방향
        public Vector3 eulerRotation; // 방향 회전

        public int phase;

        public bool bInvincibility;
        public float groggyValue; // TODO: groggyValue 이름 뭘로 바꿀지 고민해보기.

        public bool bBeginOfAnimation;
        public bool bBeginOfAction;
        public bool bEndOfAction;
        public bool bEndOfAnimation;

        private float m_health = 1;
        private float m_mana = 1;

        [HideInInspector] public List<Collider2D> sensorBuffer;

#endregion

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

            m_health = Utilities.Max<float>(1, maxHealth.finalValue);
        }

        // MonoBehaviour.Start()
        public virtual void OnStartEntity() {}
        public abstract IStateMachineBase InitStateMachine();
        public virtual bool InitActiveSelf() => true;

        // MonoBehaviour.FixedUpdate()

        // MonoBehaviour.Update()
        public virtual void OnEndOfEntity() {}

        // MonoBehaviour.LateUpdate()

        // MonoBehaviour.OnDrawGizmos()

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
    }
}