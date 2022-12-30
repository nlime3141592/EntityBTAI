using UnityEngine;

namespace UnchordMetroidvania
{
    public class _DoubleAxisDetection : _TerrainDetection
    {
        public int baseLookDirX;
        public int baseLookDirY;

        public _DoubleAxisDetection(int rayCount)
        : base(rayCount)
        {

        }
    }
}