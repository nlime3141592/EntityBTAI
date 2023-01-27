using UnityEngine;

namespace UnchordMetroidvania
{
    public class Player : EntityBase
    {
        public static Player instance => s_m_player;
        private static Player s_m_player;

        public BattleModule battleModule;
        public ElongatedHexagonCollider2D hCol;

        public PlayerTerrainSenseData senseData;
        public PlayerData data;
        public PlayerFsm fsm;

        public int CURRENT_STATE;
        public EntitySensorGizmoManager rangeGizmoManager;

        #region Player Inputs
        public bool jumpDown;
        public bool jumpUp;
        public bool rushDown;
        public bool rushUp;
        public bool parryingDown;
        public bool parryingUp;
        public bool skill00;
        public bool skill01;
        public bool skill02;
        #endregion

        public bool bIsRun = false;
        public int leftAirJumpCount = 0;
        public Vector2 cameraOffset = Vector2.zero;

        protected override void Start()
        {
            if(s_m_player != null)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                s_m_player = this;
            }

            base.Start();

            battleModule = GetComponent<BattleModule>();
            hCol = GetComponent<ElongatedHexagonCollider2D>();
            fsm = new PlayerFsm(this);

            rangeGizmoManager = new EntitySensorGizmoManager();

            fsm.Begin(fsm.idleShort);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            senseData.UpdateOrigins(hCol.head.bounds, hCol.feet.bounds);
            fsm.OnFixedUpdate();
            Debug.Log(string.Format("CurrentState: {0}", fsm.stateName));
        }

        protected override void Update()
        {
            base.Update();

            if(canInput)
            {
                base.axisInput.x = Input.GetAxisRaw("Horizontal");
                base.axisInput.y = Input.GetAxisRaw("Vertical");
                this.parryingDown = Input.GetKeyDown(KeyCode.V);
                this.parryingUp = Input.GetKeyUp(KeyCode.V);
                this.jumpDown = Input.GetKeyDown(KeyCode.Space);
                this.jumpUp = Input.GetKeyUp(KeyCode.Space);
                this.rushDown = Input.GetKeyDown(KeyCode.LeftShift);
                this.rushUp = Input.GetKeyUp(KeyCode.LeftShift);
                this.skill00 = Input.GetKeyDown(KeyCode.Z);
                this.skill01 = Input.GetKeyDown(KeyCode.X);
                this.skill02 = Input.GetKeyDown(KeyCode.C);
            }
            else
            {
                base.axisInput.x = 0;
                base.axisInput.y = 0;
                this.parryingDown = false;
                this.jumpDown = false;
                this.jumpUp = false;
                this.rushDown = false;
                this.rushUp = false;
                this.skill00 = false;
                this.skill01 = false;
                this.skill02 = false;
            }

            fsm.OnUpdate();

            CURRENT_STATE = fsm.stateId;
        }
    }
}