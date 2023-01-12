using UnityEngine;

namespace UnchordMetroidvania
{
    public class Player : EntityBase
    {
        public static Player instance => m_player;
        private static Player m_player;

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

        public bool bIsRun = false;
        public Vector2 cameraOffset = Vector2.zero;
        public int leftAirJumpCount = 0;

        public bool bOnDetectFloor;
        public bool bOnFloor;
        public bool bOnCeil;
        public bool bOnWallFrontT;
        public bool bOnWallFrontB;
        public bool bOnWallFront;
        public bool bOnWallBackT;
        public bool bOnWallBackB;
        public bool bOnWallBack;
        public bool bOnLedgeHorizontal;
        public bool bOnLedgeVertical;
        public bool bOnLedge;

        public BoxRangeBattleSkill skAttackOnFloor;
        public BoxRangeBattleSkill skAttackOnAir;
        public BoxRangeBattleSkill skAbilitySword;
        public BoxRangeBattleSkill skAbilityGun;

        public PlayerData data;
        public PlayerIdle idleLong;
        public PlayerIdleShort idleShort;
        public PlayerWalk walk;
        public PlayerRun run;
        public _PlayerSit sit;
        public _PlayerHeadUp headUp;
        public _PlayerFreeFall freeFall;
        public _PlayerGliding gliding;
        public PlayerIdleWallFront idleWallFront;
        public PlayerSlidingWallFront slidingWallFront;
        public _PlayerJumpOnFloor jumpOnFloor;
        public _PlayerJumpOnAir jumpOnAir;
        public _PlayerJumpOnWallFront jumpOnWallFront;
        public PlayerRoll roll;
        public PlayerDash dash;
        public _PlayerClimbOnLedge climbLedge;
        public PlayerAttackOnFloor attackOnFloor;
        public PlayerAttackOnAir attackOnAir;
        public PlayerAbilitySword abilitySword;
        public PlayerAbilityGun abilityGun;

        public _PlayerFSM fsm;

        public int CURRENT_STATE;
        public RangeGizmoManager rangeGizmoManager;

        #region Player Inputs
        public bool jumpDown;
        public bool jumpUp;
        public bool rushDown;
        public bool rushUp;
        public bool skill00;
        public bool skill01;
        public bool skill02;
        #endregion

        public void PublishEndOfAnimation()
        {
            fsm.OnAnimationEnd();
        }

        protected override void Start()
        {
            if(m_player != null)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                m_player = this;
            }

            base.Start();

            int state = -1;

            pAnimator = GetComponent<Animator>();
            battleModule = GetComponent<BattleModule>();
            hCol = GetComponent<ElongatedHexagonCollider2D>();

            state = -1;
            skAttackOnFloor = new BoxRangeBattleSkill("AttackOnFloor", ++state, data.attackOnFloor);
            skAttackOnAir = new BoxRangeBattleSkill("AttackOnAir", ++state, data.attackOnAir);
            skAbilitySword = new BoxRangeBattleSkill("AbilitySword", ++state, data.abilitySword);
            skAbilityGun = new BoxRangeBattleSkill("AbilityGun", ++state, data.abilityGun);
            
            // For Debugging.
            skAttackOnFloor.bRangeOnEditor = true;
            skAttackOnAir.bRangeOnEditor = true;
            skAbilitySword.bRangeOnEditor = true;
            skAbilityGun.bRangeOnEditor = true;

            state = -1;
            // data = new PlayerData();
            idleLong = new PlayerIdle(this, data, ++state, "IdleLong");
            idleShort = new PlayerIdleShort(this, data, ++state, "IdleShort");
            walk = new PlayerWalk(this, data, ++state, "Walk");
            run = new PlayerRun(this, data, ++state, "Run");
            sit = new _PlayerSit(this, data, ++state, "Sit");
            headUp = new _PlayerHeadUp(this, data, ++state, "HeadUp");
            freeFall = new _PlayerFreeFall(this, data, ++state, "FreeFall");
            gliding = new _PlayerGliding(this, data, ++state, "Gliding");
            idleWallFront = new PlayerIdleWallFront(this, data, ++state, "IdleWallFront");
            slidingWallFront = new PlayerSlidingWallFront(this, data, ++state, "SlidingWallFront");
            jumpOnFloor = new _PlayerJumpOnFloor(this, data, ++state, "JumpOnFloor");
            jumpOnAir = new _PlayerJumpOnAir(this, data, ++state, "JumpOnAir");
            jumpOnWallFront = new _PlayerJumpOnWallFront(this, data, ++state, "JumpOnWallFront");
            roll = new PlayerRoll(this, data, ++state, "Roll");
            dash = new PlayerDash(this, data, ++state, "Dash");
            climbLedge = new _PlayerClimbOnLedge(this, data, ++state, "ClimeLedge");
            attackOnFloor = new PlayerAttackOnFloor(this, data, ++state, "AttackOnFloor");
            attackOnAir = new PlayerAttackOnAir(this, data, ++state, "AttackOnAir");
            abilitySword = new PlayerAbilitySword(this, data, ++state, "AbilitySword");
            abilityGun = new PlayerAbilityGun(this, data, ++state, "AbilityGun");

            fsm = new _PlayerFSM();

            rangeGizmoManager = new RangeGizmoManager();

            fsm.Begin(idleShort);
        }

        protected override void p_Debug_OnPostInvoke()
        {
            base.p_Debug_OnPostInvoke();

            m_FixedUpdateOrigins();
            skAttackOnFloor.UpdateOptions(data.attackOnFloor);
            skAttackOnAir.UpdateOptions(data.attackOnAir);
            skAbilitySword.UpdateOptions(data.abilitySword);
            skAbilityGun.UpdateOptions(data.abilityGun);
            // vm.SetVelocityXY(base.axisInput.x * 3.0f, base.axisInput.y * 3.0f);

            fsm.OnFixedUpdate();

            skAttackOnFloor.FixedUpdateCooltime();
            skAttackOnAir.FixedUpdateCooltime();
            skAbilitySword.FixedUpdateCooltime();
            skAbilityGun.FixedUpdateCooltime();
            // Debug.Log(string.Format("CurrentState: {0}", fsm.state));
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

            CURRENT_STATE = fsm.state;
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