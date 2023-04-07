using System;

using UnityEngine;

namespace Unchord
{
    [Serializable]
    public class Transform2
    {
        public float lpx; // local position x, y
        public float lpy;
        public float ldeg; // local degree
        public bool lfx; // local flip x, y
        public bool lfy;
        public float lsx = 1; // local scale x, y
        public float lsy = 1;

        [HideInInspector] public float gpx; // global position x, y
        [HideInInspector] public float gpy;
        [HideInInspector] public bool gfx; // global flip x, y
        [HideInInspector] public bool gfy;
        [HideInInspector] public float gsx; // global scale x, y
        [HideInInspector] public float gsy;
        [HideInInspector] public float ba; // basis element a, b, c, d
        [HideInInspector] public float bb;
        [HideInInspector] public float bc;
        [HideInInspector] public float bd;
    }
}