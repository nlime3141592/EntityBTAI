using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerData
    {
        [Header("Terrain Checker Options")]
        public float detectLength = 0.5f;
        public float hitLength = 0.06f;
        public float ledgerp = 0.1f;
        public float ledgeVerticalLengthWeight = 0.6f;

        [Header("Player Move")]
        public float walkSpeed = 2.0f;
        public float runSpeed = 6.0f;

        [Header("Player Idle Short")]
        public int shortIdleFrame = 100;

        [Header("Player Free Fall")]
        public float minFreeFallSpeed = -12.0f;
        public float gravity = -49.5f;

        [Header("Player Gliding")]
        public float glidingSpeed = -3.0f;
        public float glidingAirForce = 20.0f;

        [Header("Player Sliding Wall Front")]
        public float minSlidingWallFrontSpeed = -6.0f;
        public float slidingWallGravity = -9.81f;

        [Header("Player Jump On Floor")]
        public float jumpOnFloorForce = 49.5f;
        public float jumpOnFloorSpeed = 16.0f;

        [Header("Player Jump On Air")]
        public float jumpOnAirForce = 49.5f;
        public float jumpOnAirSpeed = 20.0f;
        public int maxAirJumpCount = 1;

        [Header("Player Jump On Wall Front")]
        public float jumpOnWallForce = 49.5f;
        public float jumpOnWallSpeedX = 3.0f;
        public float jumpOnWallSpeedY = 16.0f;

        [Header("Player Roll")]
        public int rollFrame = 15;
        public float rollSpeed = 16.0f;

        [Header("Player Dash")]
        public int dashFrame = 7;
        public float dashSpeed = 32.0f;

        [Header("Skill Options")]
        public BoxRangeBattleSkillOption attackOnFloor; // 기본 공격(바닥)
        public BoxRangeBattleSkillOption attackOnAir; // 기본 공격(공중)
        public BoxRangeBattleSkillOption takeDownEarthquake; // 내려 찍기 충격파
        public BoxRangeBattleSkillOption abilitySword; // 근거리 어빌리티
        public BoxRangeBattleSkillOption abilityGun; // 원거리 어빌리티
    }
}