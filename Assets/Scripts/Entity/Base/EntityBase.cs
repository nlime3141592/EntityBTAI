using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityBase : MonoBehaviour
    {
        #region Components
        public VelocityController2D velModule;
        #endregion

        #region Terrain Detection Info
        public _GroundDetection floor;
        public _GroundDetection ceil;
        public _WallDetection wallL;
        public _WallDetection wallR;
        public _LedgeDetection ledgeLB;
        public _LedgeDetection ledgeRB;
        public _LedgeDetection ledgeLT;
        public _LedgeDetection ledgeRT;
        public bool bOnAir;
        #endregion

        #region Entity AI
        private ConfigurationBT<EntityBase> aiConfig;
        private NodeBT<EntityBase> aiRoot;

        public bool bFixLookDirX = false;
        public bool bFixLookDirY = true;
        public Vector2 lookDir = Vector2.one;
        public Vector2 moveDir;

        public bool bIsRun = false;
        #endregion

        #region Entity Inputs
        public Vector2 axisInput;
        #endregion

        #region Stat
        public Stat strength;
        public Stat defence;

        public Stat maxHealth;
        public Stat maxMana;

        public float health;
        public float mana;
        #endregion

        #region Debug
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

        protected virtual void p_OnValidate()
        {
            if(aiConfig == null) aiConfig = new ConfigurationBT<EntityBase>(this);
            if(maxHealth != null) health = maxHealth.finalValue;
            if(skillRangeGizmoManager == null) skillRangeGizmoManager = new RangeGizmoManager();
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
            ++(aiConfig.curFps);

            lookDir.x = m_GetNextLookDir(axisInput.x, lookDir.x, 1, bFixLookDirX);
            lookDir.y = m_GetNextLookDir(axisInput.y, lookDir.y, 1, bFixLookDirY);

            if(aiRoot != null)
                aiRoot.Invoke();
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