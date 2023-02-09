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
        public PlayerInputManager iManager;

        public int CURRENT_STATE;
        public EntitySensorGizmoManager rangeGizmoManager;

        #region Player Inputs
        public bool jumpDown;
        public bool jumpUp;
        public bool rushDown;
        public bool rushUp;
        public bool parryingDown;
        public bool parryingUp;
        public bool skill00; // NOTE: 일반 공격
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
            fsm = new PlayerFsm(this, 40);

            rangeGizmoManager = new EntitySensorGizmoManager();
            iManager = new PlayerInputManager(this);

            fsm.Start(PlayerFsm.c_st_IDLE_SHORT);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            senseData.UpdateOrigins(this);
            fsm.OnFixedUpdate();
            // Debug.Log(string.Format("CurrentState: {0}", fsm.stateName));
        }

        protected override void Update()
        {
            base.Update();

            iManager.UpdateInputs(canInput);
            fsm.OnUpdate();
            CURRENT_STATE = fsm.Transit();
        }
    }
}