using UnityEngine;

namespace Unchord
{
    public class ExcavatorRightArm : Entity
    {
        public const int c_st_HIDDEN = 0;
        public const int c_st_MOVE = 1;
        public const int c_st_SHOOT_HAND = 2;

        protected override IStateMachineBase InitStateMachine()
        {
            throw new System.NotImplementedException();
        }
    }
}