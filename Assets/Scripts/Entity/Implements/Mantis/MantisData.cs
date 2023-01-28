using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class MantisData
    {
        [Header("Terrain Checker Options")]
        public float wallDetectLength = 2.0f;
        public float hitLength = 0.06f;
    }
}