using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _TerrainDetection
    {
        public Transform tOrigin;
        public float dLength;
        public float hLength;
        public bool bDetected_OR;
        public bool bDetected_AND;
        public bool bHit_OR;
        public bool bHit_AND;
        public RaycastHit2D[] hits;

        public _TerrainDetection(int rayCount)
        {
            hits = new RaycastHit2D[rayCount];
        }
    }
}