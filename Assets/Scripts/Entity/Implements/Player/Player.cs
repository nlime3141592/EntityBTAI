using UnityEngine;

namespace UnchordMetroidvania
{
    public class Player : EntityBase
    {
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

        public float walkSpeed = 2.0f;
        public float runSpeed = 6.0f;
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

        public int attackCount = 0;
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

        public void PublishAttackCommand()
        {
            ++attackCount;
        }

        public bool CanReceiveAttackCommand()
        {
            if(attackCount > 0)
            {
                --attackCount;
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void Start()
        {
            base.Start();

            int state = -1;

            battleModule = GetComponent<BattleModule>();
            hCol = GetComponent<ElongatedHexagonCollider2D>();

            state = -1;
            skAttackOnFloor = new BoxRangeBattleSkill(
                "AttackOnFloor", ++state,
                data.attackOnFloor.level,
                data.attackOnFloor.targetCount,
                data.attackOnFloor.baseDamage,
                data.attackOnFloor.sortType,
                data.attackOnFloor.canDetectSelf,
                data.attackOnFloor.range
            );
            skAttackOnAir = new BoxRangeBattleSkill(
                "AttackOnAir", ++state,
                data.attackOnAir.level,
                data.attackOnAir.targetCount,
                data.attackOnAir.baseDamage,
                data.attackOnAir.sortType,
                data.attackOnAir.canDetectSelf,
                data.attackOnAir.range
            );
            skAbilitySword = new BoxRangeBattleSkill(
                "AbilitySword", ++state,
                data.abilitySword.level,
                data.abilitySword.targetCount,
                data.abilitySword.baseDamage,
                data.abilitySword.sortType,
                data.abilitySword.canDetectSelf,
                data.abilitySword.range
            );
            skAbilityGun = new BoxRangeBattleSkill(
                "AbilityGun", ++state,
                data.abilityGun.level,
                data.abilityGun.targetCount,
                data.abilityGun.baseDamage,
                data.abilityGun.sortType,
                data.abilityGun.canDetectSelf,
                data.abilityGun.range
            );
            
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

            base.axisInput.x = Input.GetAxisRaw("Horizontal");
            base.axisInput.y = Input.GetAxisRaw("Vertical");

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