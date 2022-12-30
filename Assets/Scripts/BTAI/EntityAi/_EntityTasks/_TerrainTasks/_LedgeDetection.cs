using UnityEngine;

namespace UnchordMetroidvania
{
    public class _LedgeDetection : _DoubleAxisDetection
    {
        public float height;
        public float ledgerp;

        public _LedgeDetection(int rayCount)
        : base(rayCount)
        {

        }
    }
}