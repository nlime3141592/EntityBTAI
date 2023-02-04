using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class MantisData
    {
        [Header("Terrain Checker Options")]
        public float wallDetectLength = 0.06f;
        public float hitLength = 0.06f;

        public float walkSpeed = 6.0f;
    }
}