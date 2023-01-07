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
        public float gravity = -9.81f;

        [Header("Player Gliding")]
        public float glidingSpeed = -0.3f;
        public float glidingAirForce = 20.0f;

        [Header("Player Sliding Wall Front")]
        public float minSlidingWallFrontSpeed = -1.0f;
        public float slidingWallGravity = -9.81f;
    }
}