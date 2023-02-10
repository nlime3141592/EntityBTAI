using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerFsm : EntityFsm<Player>
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
        public const int c_st_JUMP_DOWN                     = 23;

        public PlayerFsm(Player _player, int _capacity)
        : base(_player, _capacity)
        {
            PlayerData data = _player.data;
            int idx = -1;

            this[++idx] = new PlayerIdle(_player);
            this[++idx] = new PlayerIdleShort(_player);
            this[++idx] = new PlayerWalk(_player);
            this[++idx] = new PlayerRun(_player);
            this[++idx] = new PlayerSit(_player);
            this[++idx] = new PlayerHeadUp(_player);
            this[++idx] = new PlayerFreeFall(_player);
            this[++idx] = new PlayerGliding(_player);
            this[++idx] = new PlayerIdleWallFront(_player);
            this[++idx] = new PlayerSlidingWallFront(_player);
            this[++idx] = new PlayerJumpOnFloor(_player);
            this[++idx] = new PlayerJumpOnAir(_player);
            this[++idx] = new PlayerJumpOnWallFront(_player);
            this[++idx] = new PlayerRoll(_player);
            this[++idx] = new PlayerDash(_player);
            this[++idx] = new PlayerClimbOnLedge(_player);
            this[++idx] = new PlayerAttackOnFloor(_player);
            this[++idx] = new PlayerAttackOnAir(_player);
            this[++idx] = new PlayerAbilitySword(_player);
            this[++idx] = new PlayerAbilityGun(_player);
            this[++idx] = new PlayerTakeDown(_player);
            this[++idx] = new PlayerBasicParrying(_player);
            this[++idx] = new PlayerEmergencyParrying(_player);
            this[++idx] = new PlayerJumpDown(_player);
        }
    }
}