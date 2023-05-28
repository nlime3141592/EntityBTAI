/*
using UnityEngine;

namespace Unchord
{
    public class PlayerClimbOnLedge : PlayerOnLedge
    {
        private Vector2 playerPosition;
        private Vector2 playerTeleportPosition;
        private bool bInitState;

        public override void OnConstruct()
        {
            base.OnConstruct();

            idFixed = Player.c_st_CLIMB_LEDGE;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePositionX();
            instance.vm.FreezePositionY();

            bInitState = false;
            instance.bFixedLookDirByAxis.x = true;

            Vector2 hand = Vector2.zero;
            hand.x = instance.senseData.datWallFrontT.hitData.point.x;
            hand.y = instance.senseData.datCornerFrontVT.hitData.point.y;

            Vector2 dt = instance.transform.position;
            dt -= instance.senseData.datWallFrontT.origin;

            Vector2 df = instance.transform.position;
            df -= instance.senseData.datFloor.origin;

            playerPosition = hand + dt;
            playerTeleportPosition = hand + df + instance.lookDir.fx * 0.1f * Vector2.right;

            instance.transform.position = playerPosition;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Player.c_st_FREE_FALL;
            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.transform.position = playerTeleportPosition;
            instance.bFixedLookDirByAxis.x = false;
            instance.vm.MeltPositionX();
            instance.vm.MeltPositionY();
        }

        public override void OnDrawGizmoAlways()
        {
            base.OnDrawGizmoAlways();

            Debug.DrawRay(instance.senseData.datWallFrontT.origin, instance.senseData.datWallFrontT.direction * instance.senseData.datWallFrontT.dLength, Color.cyan, Time.deltaTime);
            Debug.DrawRay(instance.senseData.datCornerFrontT.origin, instance.senseData.datCornerFrontT.direction * instance.senseData.datCornerFrontT.dLength, Color.cyan, Time.deltaTime);
            Debug.DrawRay(instance.senseData.datCornerFrontVT.origin, instance.senseData.datCornerFrontVT.direction * instance.senseData.datCornerFrontVT.dLength, Color.cyan, Time.deltaTime);
        }
    }
}
*/