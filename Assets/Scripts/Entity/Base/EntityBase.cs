using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityBase : MonoBehaviour
    {
        #region Components
        [Header("Entity Components")]
        public Rigidbody2D physics;
        public VelocityController2D velModule;
        #endregion

        #region Entity AI
        [Header("Entity Action/AI")]
        public bool bFixLookDirX = false;
        public bool bFixLookDirY = true;
        public Vector2 lookDir = Vector2.one;
        public Vector2 moveDir;
        #endregion

        #region Entity Inputs
        [Header("Entity Inputs")]
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

        public void SetHealth(float h)
        {
            if(h < 0)
                health = 0;
            else if (h > maxHealth.finalValue)
                health = maxHealth.finalValue;
            else
                health = h;
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

        protected virtual void p_OnValidate()
        {
            if(maxHealth != null) health = maxHealth.finalValue;
            if(skillRangeGizmoManager == null) skillRangeGizmoManager = new RangeGizmoManager();
            
            TryGetComponent<Rigidbody2D>(out physics);

            if(TryGetComponent<VelocityController2D>(out velModule))
                velModule.Subscribe(physics);
        }

        protected virtual void Awake()
        {
            p_OnValidate();
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void FixedUpdate()
        {

        }

        protected InvokeResult FixedUpdateAI<T>(ConfigurationBT<T> config, NodeBT<T> root)
        where T : EntityBase
        {
            p_OnPreFrame();
                ++(config.curFps);
            p_OnPreInvoke();
                InvokeResult iResult = root?.Invoke() ?? InvokeResult.Failure;
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