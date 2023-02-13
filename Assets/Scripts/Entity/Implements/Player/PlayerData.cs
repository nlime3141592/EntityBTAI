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
        public float walkSpeed = 6.0f;
        public float runSpeed = 12.0f;

        [Header("Player Idle Short")]
        public int shortIdleFrame = 100;

        [Header("Player Free Fall")]
        public float minFreeFallSpeed = -35.0f;
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
        public int dashFrame = 35;
        public float dashSpeed = 32.0f;

        [Header("Player Attack On Air")]
        public int maxAirAttackCount = 1;

        [Header("Player Take Down")]
        public float takeDownSpeed = -65.0f;

        [Header("Player Jump Down")]
        public float jumpDownSpeed = 5.5f;
        public float jumpDownForce = 49.5f;
    }
}