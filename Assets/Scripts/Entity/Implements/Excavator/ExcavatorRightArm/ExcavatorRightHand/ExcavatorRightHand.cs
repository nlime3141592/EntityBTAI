using UnityEngine;

namespace Unchord
{
    public class ExcavatorRightHand : Entity
    {
        public const int c_st_HIDDEN = 0;
        public const int c_st_IDLE = 1;
        public const int c_st_SHOOT = 2;

        public override IStateMachineBase InitStateMachine()
        {
            StateMachine<ExcavatorRightHand> machine = new StateMachine<ExcavatorRightHand>(3);
            machine.instance = this;

            machine.Add(new ExcavatorRightHandHidden());
            machine.Add(new ExcavatorRightHandIdle());
            machine.Add(new ExcavatorRightHandShoot());

            machine.Begin(ExcavatorRightHand.c_st_HIDDEN);

            return machine;
        }

        public override bool InitActiveSelf() => false;
    }
}
