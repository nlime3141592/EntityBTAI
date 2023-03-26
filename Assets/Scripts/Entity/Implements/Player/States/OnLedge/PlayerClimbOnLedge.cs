using UnityEngine;

namespace Unchord
{
    public class PlayerClimbOnLedge : PlayerOnLedge
    {
        private Vector2 playerPosition;
        private Vector2 playerTeleportPosition;
        private bool bInitState;

        public override void OnMachineBegin(Player _instance, int _id)
        {
            base.OnMachineBegin(_instance, _id);

            _instance.stateMap.Add(Player.c_st_CLIMB_LEDGE, _id);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePositionX();
            instance.vm.FreezePositionY();

            bInitState = false;
            instance.bFixedLookDirByAxis.x = true;

            Vector2 rayOriginX = Vector2.zero;
            Vector2 rayOriginY = Vector2.zero;
            Vector2 dirX = Vector2.zero;
            Vector2 dirY = Vector2.zero;
            float dLength = instance.detectLength;
            float ledgerp = instance.ledgerp;
            float lWeight = instance.ledgeVerticalLengthWeight;
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");

            RaycastHit2D hitX = default(RaycastHit2D);
            RaycastHit2D hitY = default(RaycastHit2D);
            Vector2 dtPosition = Vector2.zero;
            Vector2 dfPosition = Vector2.zero;
            Vector2 handPosition = Vector2.zero;

            if(instance.lookDir.x < 0)
            {
                rayOriginX = instance.senseData.originWallLT.position;
                rayOriginY = instance.senseData.originLedgeLT.position;
                dirX = Vector2.left;
                dirY = Vector2.down;
            }
            else
            {
                rayOriginX = instance.senseData.originWallRT.position;
                rayOriginY = instance.senseData.originLedgeRT.position;
                dirX = Vector2.right;
                dirY = Vector2.down;
            }

            hitX = Physics2D.Raycast(rayOriginX, dirX, dLength, layerMask);
            hitY = Physics2D.Raycast(rayOriginY + dirX * ledgerp, dirY, dLength, layerMask);
            dtPosition = (Vector2)instance.transform.position - rayOriginX;
            dfPosition = instance.transform.position - instance.senseData.originFloor.position;
            handPosition.x = hitX.point.x;
            handPosition.y = hitY.point.y;

            playerPosition = handPosition + dtPosition;
            playerTeleportPosition = handPosition + dfPosition + dirX * ledgerp;
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
    }
}