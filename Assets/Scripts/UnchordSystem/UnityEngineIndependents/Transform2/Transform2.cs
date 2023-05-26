using System;
using System.Collections.Generic;

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

        [NonSerialized] internal float gpx; // global position x, y
        [NonSerialized] internal float gpy;
        [NonSerialized] internal bool gfx; // global flip x, y
        [NonSerialized] internal bool gfy;
        [NonSerialized] internal float gsx; // global scale x, y
        [NonSerialized] internal float gsy;
        [NonSerialized] internal float ba; // basis element a, b, c, d
        [NonSerialized] internal float bb;
        [NonSerialized] internal float bc;
        [NonSerialized] internal float bd;

        [NonSerialized] internal Transform2 parent;
        [NonSerialized] internal readonly List<Transform2> children;

        public Transform2()
        {
            children = new List<Transform2>(1);
        }
    }
}