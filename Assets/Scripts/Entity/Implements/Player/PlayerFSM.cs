using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerFsm : UnchordFsm<Player>
    {
        public const int c_st_IDLE_LONG                     = 0;
        public const int c_st_IDLE_SHORT                    = 1;
        public const int c_st_WALK                          = 2;
        public const int c_st_RUN                           = 3;
        public const int c_st_SIT                           = 4;
        public const int c_st_HEAD_UP                       = 5;
        public const int c_st_FREE_FALL                     = 6;
        public const int c_st_GLIDING                       = 7;
        public const int c_st_IDLE_WALL_FRONT               = 8;
        public const int c_st_SLIDING_WALL_FRONT            = 9;
        public const int c_st_JUMP_ON_FLOOR                 = 10;
        public const int c_st_JUMP_ON_AIR                   = 11;
        public const int c_st_JUMP_ON_WALL_FRONT            = 12;
        public const int c_st_ROLL                          = 13;
        public const int c_st_DASH                          = 14;
        public const int c_st_CLIMB_LEDGE                   = 15;
        public const int c_st_ATTACK_ON_FLOOR               = 16;
        public const int c_st_ATTACK_ON_AIR                 = 17;
        public const int c_st_ABILITY_SWORD                 = 18;
        public const int c_st_ABILITY_GUN                   = 19;
        public const int c_st_TAKE_DOWN                     = 20;
        public const int c_st_BASIC_PARRYING                = 21;
        public const int c_st_EMERGENCY_PARRYING            = 22;

        public PlayerIdle idleLong;
        public PlayerIdleShort idleShort;
        public PlayerWalk walk;
        public PlayerRun run;
        public PlayerSit sit;
        public PlayerHeadUp headUp;
        public PlayerFreeFall freeFall;
        public PlayerGliding gliding;
        public PlayerIdleWallFront idleWallFront;
        public PlayerSlidingWallFront slidingWallFront;
        public PlayerJumpOnFloor jumpOnFloor;
        public PlayerJumpOnAir jumpOnAir;
        public PlayerJumpOnWallFront jumpOnWallFront;
        public PlayerRoll roll;
        public PlayerDash dash;
        public PlayerClimbOnLedge climbLedge;
        public PlayerAttackOnFloor attackOnFloor;
        public PlayerAttackOnAir attackOnAir;
        public PlayerAbilitySword abilitySword;
        public PlayerAbilityGun abilityGun;
        public PlayerTakeDown takeDown;
        public PlayerBasicParrying basicParrying;
        public PlayerEmergencyParrying emergencyParrying;

        public PlayerFsm(Player _player)
        : base(_player)
        {
            PlayerData data = _player.data;

            idleLong = new PlayerIdle(_player, c_st_IDLE_LONG, "IdleLong");
            idleShort = new PlayerIdleShort(_player, c_st_IDLE_SHORT, "IdleShort");
            walk = new PlayerWalk(_player, c_st_WALK, "Walk");
            run = new PlayerRun(_player, c_st_RUN, "Run");
            sit = new PlayerSit(_player, c_st_SIT, "Sit");
            headUp = new PlayerHeadUp(_player, c_st_HEAD_UP, "HeadUp");
            freeFall = new PlayerFreeFall(_player, c_st_FREE_FALL, "FreeFall");
            gliding = new PlayerGliding(_player, c_st_GLIDING, "Gliding");
            idleWallFront = new PlayerIdleWallFront(_player, c_st_IDLE_WALL_FRONT, "IdleWallFront");
            slidingWallFront = new PlayerSlidingWallFront(_player, c_st_SLIDING_WALL_FRONT, "SlidingWallFront");
            jumpOnFloor = new PlayerJumpOnFloor(_player, c_st_JUMP_ON_FLOOR, "JumpOnFloor");
            jumpOnAir = new PlayerJumpOnAir(_player, c_st_JUMP_ON_AIR, "JumpOnAir");
            jumpOnWallFront = new PlayerJumpOnWallFront(_player, c_st_JUMP_ON_WALL_FRONT, "JumpOnWallFront");
            roll = new PlayerRoll(_player, c_st_ROLL, "Roll");
            dash = new PlayerDash(_player, c_st_DASH, "Dash");
            climbLedge = new PlayerClimbOnLedge(_player, c_st_CLIMB_LEDGE, "ClimeLedge");
            attackOnFloor = new PlayerAttackOnFloor(_player, c_st_ATTACK_ON_FLOOR, "AttackOnFloor");
            attackOnAir = new PlayerAttackOnAir(_player, c_st_ATTACK_ON_AIR, "AttackOnAir");
            abilitySword = new PlayerAbilitySword(_player, c_st_ABILITY_SWORD, "AbilitySword");
            abilityGun = new PlayerAbilityGun(_player, c_st_ABILITY_GUN, "AbilityGun");
            takeDown = new PlayerTakeDown(_player, c_st_TAKE_DOWN, "TakeDown");
            basicParrying = new PlayerBasicParrying(_player, c_st_BASIC_PARRYING, "BasicParrying");
            emergencyParrying = new PlayerEmergencyParrying(_player, c_st_EMERGENCY_PARRYING, "EmergencyParrying");
        }

        public override bool OnUpdate()
        {
            attackOnFloor.UpdateCoyoteTime();

            attackOnFloor.UpdateCooltime();
            attackOnAir.UpdateCooltime();
            abilitySword.UpdateCooltime();
            abilityGun.UpdateCooltime();

            return base.OnUpdate();
        }
    }
}