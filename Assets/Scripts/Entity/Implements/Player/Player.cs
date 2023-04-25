using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class Player : Entity
    {
        public const int c_st_IDLE_LONG                     = 0;
        public const int c_st_IDLE_SHORT                    = 1;
        public const int c_st_WALK                          = 2;
        public const int c_st_RUN                           = 3;
        public const int c_st_SIT                           = 4;
        public const int c_st_HEAD_UP                       = 5;
        public const int c_st_FREE_FALL                     = 6;
        // DEPRECATED c_st_GLIDING                       = 7;
        public const int c_st_IDLE_WALL_FRONT               = 8;
        public const int c_st_SLIDING_WALL_FRONT            = 9;
        public const int c_st_JUMP_ON_FLOOR                 = 10;
        public const int c_st_JUMP_ON_AIR                   = 11;
        public const int c_st_JUMP_ON_WALL_FRONT            = 12;
        public const int c_st_ROLL                          = 13;
        public const int c_st_DASH                          = 14;
        public const int c_st_CLIMB_LEDGE                   = 15;
        // DEPRECATED c_st_ATTACK_ON_FLOOR               = 16;
        // DEPRECATED c_st_ATTACK_ON_AIR                 = 17;
        public const int c_st_ABILITY_SWORD                 = 18;
        public const int c_st_ABILITY_GUN                   = 19;
        public const int c_st_TAKE_DOWN                     = 20;
        public const int c_st_BASIC_PARRYING                = 21;
        public const int c_st_EMERGENCY_PARRYING            = 22;
        public const int c_st_JUMP_DOWN                     = 23;
        public const int c_st_ATTACK_ON_FLOOR_001           = 24;
        public const int c_st_ATTACK_ON_FLOOR_002           = 25;
        public const int c_st_ATTACK_ON_FLOOR_003           = 26;
        public const int c_st_ATTACK_ON_AIR_001             = 27;
        public const int c_st_ATTACK_ON_AIR_002             = 28;
        public const int c_st_TAKE_DOWN_001                 = 29;
        public const int c_st_TAKE_DOWN_002                 = 30;
        public const int c_st_TAKE_DOWN_003                 = 31;

        public static Player instance => s_m_player;
        private static Player s_m_player;

#region Player Components
        public BattleModule battleModule;
        public ElongatedHexagonCollider2D hCol;
#endregion

#region Player State Machine Management
        public IStateMachine<Player> fsm;
        private IState<Player> m_stateTree;
#endregion

#region Player Datas
        public float ledgerp = 0.06f;
        public float ledgeVerticalLengthWeight = 0.5f;

        public float speed_Walk = 6.0f;
        public float speed_Run = 12.0f;
        public float speed_JumpOnFloor = 16.0f;
        public float speed_JumpOnAir = 20.0f;
        public float speed_JumpOnWall_X = 3.0f;
        public float speed_JumpOnWall_Y = 16.0f;
        public float speed_Roll = 16.0f;
        public float speed_Dash = 32.0f;
        public float speed_AttackOnAir = 1.5f;
        public float speed_TakeDown = 65.0f;
        public float speed_JumpDown = 5.5f;

        public float speedMin_FreeFall = -35.0f;
        public float speedMin_WallSlidingFront = -6.0f;

        public float force_JumpOnFloor = 49.5f;
        public float force_JumpOnAir = 49.5f;
        public float force_JumpOnWall = 49.5f;
        public float force_JumpDown = 49.5f;

        public float gravity_FreeFall = -49.5f;
        public float gravity_WallSlidingFront = -9.81f;
        public float gravity_AttackOnAir = -49.5f;

        public int frame_IdleShort = 100;

        public int count_JumpOnAir = 1;
        public int count_Dash = 1;
        public int count_AttackOnAir = 1;

        public Sensor_SO skillRange_AttackOnFloor001;
        public Sensor_SO skillRange_AttackOnFloor002;
        public Sensor_SO skillRange_AttackOnFloor003;
#endregion

#region Player Variables
        public bool bIsRun = false;

        public int countLeft_JumpOnAir = 0;
        public int countLeft_Dash = 0;
        public int countLeft_AttackOnAir = 0;

        public Vector2 offset_StandCamera;

        public TimerHandler timerCoyote_AttackOnFloor;
        public int stateNext_AttackOnFloor;

        public TimerHandler timerCoyote_AttackOnAir;
        public int stateNext_AttackOnAir;
#endregion

#region 아직 정리 안 함.
        // TODO: 아직 어떤 Region에 넣을지 결정하지 못함.
        public PlayerTerrainSensor senseData;
        public PlayerInputManager iManager;

        public int CURRENT_STATE;
        public string CURRENT_TYPE;
        public float DEBUG_COYOTE;
        public EntitySensorGizmoManager rangeGizmoManager;

        public int aPhase;
#endregion

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

        public List<Slab> sitSlabs;

        protected override bool InitSingletonInstance()
        {
            if(s_m_player == null)
                s_m_player = this;
            if(s_m_player == this)
                return true;

            return false;
        }

        protected override void InitComponents()
        {
            base.InitComponents();

            battleModule = GetComponent<BattleModule>();
            hCol = GetComponent<ElongatedHexagonCollider2D>();

            timerCoyote_AttackOnFloor = new TimerHandler();
            timerCoyote_AttackOnAir = new TimerHandler();
        }

        protected override void InitMiscellaneous()
        {
            base.InitMiscellaneous();

            rangeGizmoManager = new EntitySensorGizmoManager();
            iManager = new PlayerInputManager(this);
        }

        protected override IStateMachineBase InitStateMachine()
        {
            base.InitStateMachine();

            fsm = new StateMachine<Player>(30);

            StateComposite<Player> root = new StateComposite<Player>(30);
            m_stateTree = root;

            StateComposite<Player> state_AttackOnFloor = new StateComposite<Player>(3);
            state_AttackOnFloor[0] = new PlayerAttackOnFloor001();
            state_AttackOnFloor[1] = new PlayerAttackOnFloor002();
            state_AttackOnFloor[2] = new PlayerAttackOnFloor003();

            StateComposite<Player> state_AttackOnAir = new StateComposite<Player>(2);
            state_AttackOnAir[0] = new PlayerAttackOnAir001();
            state_AttackOnAir[1] = new PlayerAttackOnAir002();

            StateComposite<Player> state_TakeDown = new StateComposite<Player>(3);
            state_TakeDown[0] = new PlayerTakeDown001();
            state_TakeDown[1] = new PlayerTakeDown002();
            state_TakeDown[2] = new PlayerTakeDown003();

            // NOTE: 상태를 이 곳에서 조직하고, m_stateTree에 Root 할당하기.
            int index_root = -1;
            root[++index_root] = new PlayerIdleLong();
            root[++index_root] = new PlayerIdleShort();
            root[++index_root] = new PlayerWalk();
            root[++index_root] = new PlayerRun();
            root[++index_root] = new PlayerSit();
            root[++index_root] = new PlayerHeadUp();
            root[++index_root] = new PlayerFreeFall();
            root[++index_root] = new PlayerIdleWallFront();
            root[++index_root] = new PlayerSlidingWallFront();
            root[++index_root] = new PlayerJumpOnFloor();
            root[++index_root] = new PlayerJumpOnAir();
            root[++index_root] = new PlayerJumpOnWallFront();
            root[++index_root] = new PlayerRoll();
            root[++index_root] = new PlayerDash();
            // root[++index_root] = new PlayerClimbOnLedge();
            root[++index_root] = state_AttackOnFloor;
            root[++index_root] = state_AttackOnAir;
            // root[++index_root] = new PlayerAbilitySword();
            // root[++index_root] = new PlayerAbilityGun();
            root[++index_root] = state_TakeDown;
            root[++index_root] = new PlayerBasicParrying();
            root[++index_root] = new PlayerEmergencyParrying();
            root[++index_root] = new PlayerJumpDown();

            stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_001;
            stateNext_AttackOnAir = Player.c_st_ATTACK_ON_AIR_001;

            // timer handler settings
            timerCoyote_AttackOnFloor.onEndOfTimer += () => { stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_001; };
            timerCoyote_AttackOnAir.onEndOfTimer += () => { stateNext_AttackOnAir = Player.c_st_ATTACK_ON_AIR_001; };
            RegisterTimerHandler(timerCoyote_AttackOnFloor);
            RegisterTimerHandler(timerCoyote_AttackOnAir);

            fsm.Begin(root, Player.c_st_IDLE_SHORT);
            return fsm;
        }

        protected override void PreUpdate()
        {
            base.PreUpdate();

            iManager.UpdateInputs(true);
        }

        protected override void PostUpdate()
        {
            base.PostUpdate();

            // CURRENT_STATE = machineInterface.state;
            CURRENT_TYPE = machineInterface.state.GetType().ToString();
        }

        protected override bool bShowGizmos()
        {
            return true;
        }
/*
        protected override void Start()
        {
            // NOTE: 각 State별 OnMachineBegin() 함수에 이 코드를 집어넣을 것.
            leftAirJumpCount = data.maxAirJumpCount;
            leftDashCount = data.maxDashCount;
            leftAirAttackCount = data.maxAirAttackCount;
        }
*/
/*
        protected override void FixedUpdate()
        {
            // NOTE: PlayerState.OnFixedUpdate() 함수에 넣을 것.
            senseData.UpdateOrigins(this);
        }
*/
/*
        protected override void Update()
        {
            // NOTE: PlayerState.OnUpdate() 함수에 넣을 것.
            iManager.UpdateInputs(canInput);

            if(Input.GetKeyDown(KeyCode.F5))
                bIsRun = !bIsRun;
        }
*/
/*
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if(Application.isPlaying)
            {
                ((PlayerJumpDown)(fsm[PlayerFsm.c_st_JUMP_DOWN])).m_sitRange.Draw(transform.position, false, false, Color.red);
                ((PlayerJumpDown)(fsm[PlayerFsm.c_st_JUMP_DOWN])).m_slabRange.Draw(transform.position, false, false, Color.cyan);
            }
        }
*/
    }
}