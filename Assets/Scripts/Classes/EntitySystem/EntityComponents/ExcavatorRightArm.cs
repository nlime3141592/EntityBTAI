using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/Entity Components/Excavator Right Arm")]
    public class ExcavatorRightArm : Entity
    {
        public const int c_st_HIDDEN = 0;
        public const int c_st_MOVE = 1;
        public const int c_st_SHOOT_HAND = 2;

        // organ hierarchy
        public ExcavatorRightHand rightHand;

        public int nextState = -1;

        public float degSpeed_Tracking = 45.0f;
        public Vector2 offset_TrackingBeg;
        public Vector2 offset_TrackingEnd;
        public Entity target_Tracking;

        public Vector2 offset_rightArmJoint002;
        public Vector2 position_rightArmJoint001;
        public Vector2 position_rightArmJoint002;

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