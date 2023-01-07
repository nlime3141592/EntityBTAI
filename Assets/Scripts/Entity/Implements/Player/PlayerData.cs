using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerData
    {
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
        public float rollFrame = 45;
        public float rollSpeed = 16.0f;
    }
}