using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _EntityBase : MonoBehaviour
    {
        public VelocityController2D velModule;

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

        public Vector2 moveDir;
        public Vector2 lookDir;
        #endregion

        #region Entity Input
        public Vector2 axisInput;
        #endregion

        #region Stat
        public Stat strength;
        public Stat defence;
        public Stat maxHealth;
        public Stat mana;

        public float health;
        public float testMoveSpeed = 3.0f;
        #endregion

        public RangeGizmoManager skillRangeGizmoManager;

        public void SetHealth(float value)
        {
            if(value < 0)
                health = 0;
            else if(value > maxHealth.finalValue)
                health = maxHealth.finalValue;
            else
                health = value;
        }

        protected virtual void OnValidate()
        {
            if(!Application.isEditor || Application.isPlaying)
                return;

            if(maxHealth != null)
                health = maxHealth.finalValue;
        }

        protected virtual void Start()
        {
            skillRangeGizmoManager = new RangeGizmoManager();
        }

        protected virtual void FixedUpdate()
        {
            lookDir.x = FixedUpdateLookDir(axisInput.x, lookDir.x, false);
            // lookDir.y = FixedUpdateLookDir(axisInput.y, lookDir.y, false);
            lookDir.y = 1;

            // TEST MOVEMENT.
            float finalSpeed = testMoveSpeed * Time.fixedDeltaTime;
            float dx = axisInput.x;
            float dy = axisInput.y;
            transform.position += new Vector3(dx * finalSpeed, dy * finalSpeed);
        }

        protected virtual void Update()
        {

        }

        protected virtual void OnDrawGizmos()
        {
            if(skillRangeGizmoManager != null)
                skillRangeGizmoManager.OnDrawGizmos(Time.deltaTime);
        }

        private float FixedUpdateLookDir(float axisValue, float curLookDir, bool bFixed)
        {
            if(bFixed)
                return curLookDir;
            else if(axisValue < 0)
                return -1;
            else if(axisValue > 0)
                return 1;
            else
                return curLookDir;
        }
    }
}