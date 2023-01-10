using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityBase : MonoBehaviour
    {
        #region Components
        [Header("Entity Components")]
        public Rigidbody2D physics;
        public TerrainSensor sensor;
        public VelocityModule2D vm;
        public List<Collider2D> hitColliders;
        #endregion

        #region Entity AI
        [Header("Entity Action/AI")]
        public bool bFixLookDirX = false;
        public bool bFixLookDirY = true;
        public Vector2 lookDir = Vector2.one;
        public Vector2 moveDir;
        public InvokeResult lastInvokeResult;
        public bool bEndOfEntity = false;

        private object m_root;
        private Func<InvokeResult> m_func_rootInvoke;
        private bool m_bHasAI = false;
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

        public float health;
        public float mana;
        #endregion

        #region Debug
        [Header("Debug Options")]
        public RangeGizmoManager skillRangeGizmoManager;
        #endregion

        private void OnValidate()
        {
            if(!Application.isEditor || Application.isPlaying)
                return;

            p_OnValidate();
        }

        public float SetHealth(float h)
        {
            float finalHealth = (float)Math.Round(h, 4);

            if(finalHealth < 0)
                finalHealth = 0;
            else if (finalHealth > maxHealth.finalValue)
                finalHealth = maxHealth.finalValue;

            float dH = Math.Abs(health - finalHealth);
            health = finalHealth;
            return dH;
        }

        public void FixConstraints(bool posX, bool posY)
        {
            if(posX)
                physics.constraints |= RigidbodyConstraints2D.FreezePositionX;
            else
                physics.constraints &= ~(RigidbodyConstraints2D.FreezePositionX);

            if(posY)
                physics.constraints |= RigidbodyConstraints2D.FreezePositionY;
            else
                physics.constraints &= ~(RigidbodyConstraints2D.FreezePositionY);
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

        protected virtual void p_OnValidate()
        {
            if(maxHealth != null) health = maxHealth.finalValue;
            if(skillRangeGizmoManager == null) skillRangeGizmoManager = new RangeGizmoManager();
        }

        protected virtual void Awake()
        {
            p_OnValidate();
        }

        protected virtual void Start()
        {
            hitColliders = new List<Collider2D>();
            TryGetComponent<Rigidbody2D>(out physics);
            sensor = new TerrainSensor();
            vm = new VelocityModule2D(physics);
        }

        private void FixedUpdate()
        {
            // Rotation Logic
            if(lookDir.x < 0)
                transform.eulerAngles = Vector3.up * 180;
            else if(lookDir.x > 0)
                transform.eulerAngles = Vector3.zero;

            lastInvokeResult = m_FixedUpdateAI();
            p_Debug_OnPostInvoke();
        }

        protected void RegisterAI<T>(NodeBT<T> root)
        where T : EntityBase
        {
            m_root = root;
            m_func_rootInvoke = root.Invoke;
            m_bHasAI = true;
        }

        protected virtual void p_Debug_OnPostInvoke()
        {
            lookDir.x = m_GetNextLookDir(axisInput.x, lookDir.x, 1, bFixLookDirX);
            lookDir.y = m_GetNextLookDir(axisInput.y, lookDir.y, 1, bFixLookDirY);
        }

        private InvokeResult m_FixedUpdateAI()
        {
            if(!m_bHasAI)
                return InvokeResult.Failure;

            p_OnPreInvoke();
                InvokeResult iResult = m_func_rootInvoke();
            p_OnPostInvoke();
                return iResult;
        }

        protected virtual void p_OnPreFrame()
        {

        }

        protected virtual void p_OnPreInvoke()
        {
            lookDir.x = m_GetNextLookDir(axisInput.x, lookDir.x, 1, bFixLookDirX);
            lookDir.y = m_GetNextLookDir(axisInput.y, lookDir.y, 1, bFixLookDirY);
        }

        protected virtual void p_OnPostInvoke()
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

        private float m_GetNextLookDir(float axisInputValue, float curLookDir, float defaultLookDir, bool bFixLookDir)
        {
            if(curLookDir != -1 && curLookDir != 1)
                return defaultLookDir;
            else if(bFixLookDir)
                return curLookDir;
            else if(axisInputValue < 0)
                return -1;
            else if(axisInputValue > 0)
                return 1;
            else
                return curLookDir;
        }
    }
}