using UnityEngine;

namespace Unchord
{
    public class ExcavatorRightHand : Entity
    {
        public const int c_st_HIDDEN = 0;
        public const int c_st_IDLE = 1;
        public const int c_st_SHOOT = 2;

        protected override IStateMachineBase InitStateMachine()
        {
            throw new System.NotImplementedException();
        }
    }
}
