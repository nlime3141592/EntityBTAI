using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityBase : MonoBehaviour
    {
        #region Components
        [Header("Entity Components")]
        [HideInInspector] public Rigidbody2D physics;
        [HideInInspector] public AnimationController aController;
        public List<Collider2D> hitColliders;
        public List<SpriteRenderer> spRenderers;
        public VelocityModule2D vm;
        #endregion

        #region Entity AI
        [Header("Entity Action/AI")]
        public bool bFixLookDirX = false;
        public bool bFixLookDirY = true;
        public Vector2 lookDir = Vector2.one;
        public Vector2 moveDir;
        public InvokeResult lastInvokeResult;
        public bool bBeginOfEntity = false;
        public bool bEndOfEntity = false; // 사망 조건을 만족한 상태에서 이 변수가 true가 되는 순간 엔티티 게임오브젝트가 파괴됩니다.

        private object m_root;
        private Func<InvokeResult> m_func_rootInvoke;
        #endregion

        #region Entity Events
        public event EntityEvent<EntityHealArgs> onHeal;
        public event EntityEvent<EntityDamageArgs> onDamage;
        public event EntityEvent<EntityChargeArgs> onCharge;
        public event EntityEvent<EntityExpenseArgs> onExpense;
        #endregion

        #region Entity Inputs
        [Header("Entity Inputs")]
        public bool canInput = true;
        public Vector2 axisInput;
        #endregion

        #region Stat
        [Header("Entity Stats")]
        public Stat strength;
        public Stat defence;
        public Stat maxHealth;
        public Stat maxMana;
        public Stat baseMoveSpeed;
        public Stat baseGravity;
        public Stat baseMentality;

        public float health;
        public float mana;
        public float groggyValue = 0.0f;

        public bool bInvincibility = false;
        public float fixTakenDamage = 0.0f;
        #endregion

        #region Debug
        [Header("Debug Options")]
        public EntitySensorGizmoManager skillRangeGizmoManager;
        #endregion

        public float Heal(float dHealth)
        {
            if(dHealth <= 0)
                return 0;

            EntityHealArgs args = new EntityHealArgs();
            args.minHealth = 0;
            args.maxHealth = maxHealth.finalValue;
            args.prevHealth = health;
            args.currentHealth = m_ClampValue(health + dHealth, 0, args.maxHealth);
            args.dHealthOriginal = dHealth;
            health = args.currentHealth;

            if(onHeal != null)
                onHeal(this, args);

            return args.dHealthReal;
        }

        public float Damage(float dHealth)
        {
            if(dHealth <= 0)
                return 0;

            EntityDamageArgs args = new EntityDamageArgs();
            args.minHealth = 0;
            args.maxHealth = maxHealth.finalValue;
            args.prevHealth = health;
            args.currentHealth = m_ClampValue(health - dHealth, 0, args.maxHealth);
            args.dHealthOriginal = dHealth;
            health = args.currentHealth;

            if(onDamage != null)
                onDamage(this, args);

            return args.dHealthReal;
        }

        public float Charge(float dMana)
        {
            if(dMana <= 0)
                return 0;

            EntityChargeArgs args = new EntityChargeArgs();
            args.minMana = 0;
            args.maxMana = maxMana.finalValue;
            args.prevMana = mana;
            args.currentMana = m_ClampValue(mana + dMana, 0, args.maxMana);
            args.dManaOriginal = dMana;
            mana = args.currentMana;

            if(onCharge != null)
                onCharge(this, args);

            return args.dManaReal;
        }

        public float Expense(float dMana)
        {
            if(dMana <= 0)
                return 0;

            EntityExpenseArgs args = new EntityExpenseArgs();
            args.minMana = 0;
            args.maxMana = maxMana.finalValue;
            args.prevMana = mana;
            args.currentMana = m_ClampValue(mana - dMana, 0, args.maxMana);
            args.dManaOriginal = dMana;
            mana = args.currentMana;

            if(onExpense != null)
                onExpense(this, args);

            return args.dManaReal;
        }

        private float m_ClampValue(float value, float min, float max)
        {
            if(value < min)
                return min;
            else if(value > max)
                return max;
            else
                return (float)Math.Round(value, 4);
        }

        // NOTE:
        // 엔티티 사망 애니메이션이 끝날 때 이 함수를 호출하세요.
        // 애니메이션 이벤트 기능을 사용합니다.
        public void OnEntityDeadAnimationEnd()
        {
            bEndOfEntity = true;
        }

        public virtual void OnEntityDestroy()
        {

        }

        private void OnValidate()
        {
            if(!Application.isEditor || Application.isPlaying)
                return;

            p_OnValidate();
        }

        protected virtual void p_OnValidate()
        {
            if(maxHealth != null) health = maxHealth.finalValue;
            if(maxMana != null) mana = maxMana.finalValue;
        }

        protected virtual void Awake()
        {
            p_OnValidate();
        }

        protected virtual void Start()
        {
            skillRangeGizmoManager = new EntitySensorGizmoManager();
            // hitColliders = new List<Collider2D>();
            TryGetComponent<Rigidbody2D>(out physics);
            vm = new VelocityModule2D(physics);
            TryGetComponent<AnimationController>(out aController);

            aController.OnStart();
        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void Update()
        {
            canInput = GameManager.instance.bGameStarted;

            for(int i = 0; i < hitColliders.Count; ++i)
                hitColliders[i].enabled = health > 0;
        }

        protected virtual void OnDrawGizmos()
        {
            if(skillRangeGizmoManager != null)
                skillRangeGizmoManager.OnDrawGizmos(Time.deltaTime);
        }
    }
}