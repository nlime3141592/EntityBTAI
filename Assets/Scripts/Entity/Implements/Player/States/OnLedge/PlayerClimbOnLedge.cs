using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerClimbOnLedge : PlayerOnLedge
    {
        private Vector2 playerPosition;
        private Vector2 playerTeleportPosition;
        private bool bInitState;

        public PlayerClimbOnLedge(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            player.vm.FreezePositionX();
            player.vm.FreezePositionY();

            bInitState = false;
            player.bFixLookDirX = true;

            Vector2 rayOriginX = Vector2.zero;
            Vector2 rayOriginY = Vector2.zero;
            Vector2 dirX = Vector2.zero;
            Vector2 dirY = Vector2.zero;
            float dLength = data.detectLength;
            float ledgerp = data.ledgerp;
            float lWeight = data.ledgeVerticalLengthWeight;
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");

            RaycastHit2D hitX = default(RaycastHit2D);
            RaycastHit2D hitY = default(RaycastHit2D);
            Vector2 dtPosition = Vector2.zero;
            Vector2 dfPosition = Vector2.zero;
            Vector2 handPosition = Vector2.zero;

            if(player.lookDir.x < 0)
            {
                rayOriginX = player.senseData.originWallLT.position;
                rayOriginY = player.senseData.originLedgeLT.position;
                dirX = Vector2.left;
                dirY = Vector2.down;
            }
            else
            {
                rayOriginX = player.senseData.originWallRT.position;
                rayOriginY = player.senseData.originLedgeRT.position;
                dirX = Vector2.right;
                dirY = Vector2.down;
            }

            hitX = Physics2D.Raycast(rayOriginX, dirX, dLength, layerMask);
            hitY = Physics2D.Raycast(rayOriginY + dirX * ledgerp, dirY, dLength, layerMask);
            dtPosition = (Vector2)player.transform.position - rayOriginX;
            dfPosition = player.transform.position - player.senseData.originFloor.position;
            handPosition.x = hitX.point.x;
            handPosition.y = hitY.point.y;

            playerPosition = handPosition + dtPosition;
            playerTeleportPosition = handPosition + dfPosition + dirX * ledgerp;
            bInitState = true;
        }

        public override void OnFixedUpdate()
        {
            if(bInitState)
            {
                player.transform.position = playerPosition;
            }
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.aController.bEndOfAnimation)
            {
                player.transform.position = playerTeleportPosition;
                fsm.Change(fsm.freeFall);
                return true;
            }


            // NOTE: ????????? ??????.
            else if(Input.GetKeyDown(KeyCode.Return))
            {
                player.aController.bEndOfAnimation = true;
                return false;
            }

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.bFixLookDirX = false;
            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }
    }
}