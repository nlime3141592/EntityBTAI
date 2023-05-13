using System;
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

        public float time_idleShort = 10.0f;

        public int frame_IdleShort = 100;

        public int count_JumpOnAir = 1;
        public int count_Dash = 1;
        public int count_AttackOnAir = 1;

        public AreaSensorBox skillRange_AttackOnFloor001_01;
        public AreaSensorBox skillRange_AttackOnFloor002_01;
        public AreaSensorBox skillRange_AttackOnFloor003_01;
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

        public string CURRENT_TYPE;
        public float DEBUG_COYOTE;
#endregion

        public List<Slab> sitSlabs;

        public override bool InitSingletonInstance()
        {
            if(s_m_player == null)
                s_m_player = this;
            if(s_m_player == this)
                return true;

            return false;
        }

        public override void OnAwakeEntity()
        {
            base.OnAwakeEntity();

            battleModule = GetComponent<BattleModule>();
            hCol = GetComponent<ElongatedHexagonCollider2D>();

            timerCoyote_AttackOnFloor = new TimerHandler();
            timerCoyote_AttackOnAir = new TimerHandler();

            iManager = new PlayerInputManager();
        }

        public override IStateMachineBase InitStateMachine()
        {
            StateMachine<Player> fsm = new StateMachine<Player>(50);
            fsm.instance = this;

            fsm.Add(new PlayerIdleLong());
            fsm.Add(new PlayerIdleShort());
            fsm.Add(new PlayerWalk());
            fsm.Add(new PlayerRun());
            fsm.Add(new PlayerSit());
            fsm.Add(new PlayerHeadUp());
            fsm.Add(new PlayerFreeFall());
            fsm.Add(new PlayerIdleWallFront());
            fsm.Add(new PlayerSlidingWallFront());
            fsm.Add(new PlayerJumpOnFloor());
            fsm.Add(new PlayerJumpOnAir());
            fsm.Add(new PlayerJumpOnWallFront());
            fsm.Add(new PlayerRoll());
            fsm.Add(new PlayerDash());
            fsm.Add(new PlayerAttackOnFloor001());
            fsm.Add(new PlayerAttackOnFloor002());
            fsm.Add(new PlayerAttackOnFloor003());
            fsm.Add(new PlayerAttackOnAir001());
            fsm.Add(new PlayerAttackOnAir002());
            fsm.Add(new PlayerTakeDown001());
            fsm.Add(new PlayerTakeDown002());
            fsm.Add(new PlayerTakeDown003());
            fsm.Add(new PlayerBasicParrying());
            fsm.Add(new PlayerEmergencyParrying());
            fsm.Add(new PlayerJumpDown());

            stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_001;
            stateNext_AttackOnAir = Player.c_st_ATTACK_ON_AIR_001;

            // timer handler settings
            timerCoyote_AttackOnFloor.onEndOfTimer += () => { stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_001; };
            timerCoyote_AttackOnAir.onEndOfTimer += () => { stateNext_AttackOnAir = Player.c_st_ATTACK_ON_AIR_001; };

            fsm.Begin(Player.c_st_IDLE_SHORT);
            return fsm;
        }
    }
}