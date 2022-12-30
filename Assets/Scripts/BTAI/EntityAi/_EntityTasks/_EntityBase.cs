using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _EntityBase : MonoBehaviour
    {
        public VelocityController2D velModule;

        #region Terrain Detection Info
        public _GroundDetection floor;
        public _GroundDetection ceil;
        public _WallDetection wallL;
        public _WallDetection wallR;
        public _LedgeDetection ledgeLB;
        public _LedgeDetection ledgeRB;
        public _LedgeDetection ledgeLT;
        public _LedgeDetection ledgeRT;
        public bool bOnAir;

        public Vector2 moveDir;
        #endregion
    }
}