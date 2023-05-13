using UnityEngine;

namespace Unchord
{
    public class ExcavatorRightArm : Entity
    {
        public const int c_st_HIDDEN = 0;
        public const int c_st_MOVE = 1;
        public const int c_st_SHOOT_HAND = 2;

        // organ hierarchy
        public ExcavatorRightHand rightHand;

        public override void OnAwakeEntity()
        {
            base.OnAwakeEntity();

            rightHand = gameObject.GetComponentInChildren<ExcavatorRightHand>(true);
        }

        public override IStateMachineBase InitStateMachine()
        {
            StateMachine<ExcavatorRightArm> machine = new StateMachine<ExcavatorRightArm>(3);
            machine.instance = this;

            machine.Add(new ExcavatorRightArmHidden());
            machine.Add(new ExcavatorRightArmMove());
            machine.Add(new ExcavatorRightArmShootHand());

            machine.Begin(ExcavatorRightArm.c_st_HIDDEN);

            return machine;
        }

        public override bool InitActiveSelf() => false;
    }
}