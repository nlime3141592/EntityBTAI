using UnityEngine;

namespace UnchordMetroidvania
{
    public class Player : EntityBase
    {
        public static Player instance => s_m_player;
        private static Player s_m_player;

        public Animator pAnimator;
        public BattleModule battleModule;
        public ElongatedHexagonCollider2D hCol;
        public Transform originFloor;
        public Transform originCeil;
        public Transform originWallLT;
        public Transform originWallRT;
        public Transform originWallLB;
        public Transform originWallRB;
        public Transform originLedgeLT;
        public Transform originLedgeRT;
        public Transform originLedgeLB;
        public Transform originLedgeRB;

        public PlayerFSM fsm;

        public int CURRENT_STATE;
        public EntitySensorGizmoManager rangeGizmoManager;

        #region Player Inputs
        public bool jumpDown;
        public bool jumpUp;
        public bool rushDown;
        public bool rushUp;
        public bool skill00;
        public bool skill01;
        public bool skill02;
        #endregion

        public void ChangeAnimation(PlayerState state)
        {
            pAnimator.SetInteger("state", state.id);
        }

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

            pAnimator = GetComponent<Animator>();
            battleModule = GetComponent<BattleModule>();
            hCol = GetComponent<ElongatedHexagonCollider2D>();
            fsm = GetComponent<PlayerFSM>();

            rangeGizmoManager = new EntitySensorGizmoManager();

            fsm.OnStart();
            fsm.Begin(fsm.idleShort);
        }

        protected override void p_Debug_OnPostInvoke()
        {
            base.p_Debug_OnPostInvoke();

            m_FixedUpdateOrigins();

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
                this.jumpDown = false;
                this.jumpUp = false;
                this.rushDown = false;
                this.rushUp = false;
                this.skill00 = false;
                this.skill01 = false;
                this.skill02 = false;
            }

            fsm.OnUpdate();

            CURRENT_STATE = fsm.stateID;
        }

        private void m_FixedUpdateOrigins()
        {
            Bounds head = hCol.head.bounds;
            Bounds feet = hCol.feet.bounds;

            originFloor.position = new Vector2(feet.center.x, feet.min.y);
            originCeil.position = new Vector2(head.center.x, head.max.y);

            originWallLT.position = new Vector2(head.min.x, head.center.y);
            originWallRT.position = new Vector2(head.max.x, head.center.y);
            originWallLB.position = new Vector2(feet.min.x, feet.center.y);
            originWallRB.position = new Vector2(feet.max.x, feet.center.y);

            originLedgeLT.position = new Vector2(head.min.x, head.max.y);
            originLedgeRT.position = new Vector2(head.max.x, head.max.y);
            originLedgeLB.position = new Vector2(feet.min.x, feet.min.y);
            originLedgeRB.position = new Vector2(feet.max.x, feet.min.y);
        }
    }
}