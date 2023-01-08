using UnityEngine;

namespace UnchordMetroidvania
{
    public class _PlayerClimbOnLedge : _PlayerOnLedge
    {
        private Vector2 playerPosition;
        private Vector2 playerTeleportPosition;
        private bool bInitState;

        public _PlayerClimbOnLedge(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

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
            Vector2 dPosition = Vector2.zero;
            Vector2 handPosition = Vector2.zero;

            if(player.lookDir.x < 0)
            {
                rayOriginX = player.originWallLT.position;
                rayOriginY = player.originLedgeLT.position;
                dirX = Vector2.left;
                dirY = Vector2.down;
            }
            else
            {
                rayOriginX = player.originWallRT.position;
                rayOriginY = player.originLedgeRT.position;
                dirX = Vector2.right;
                dirY = Vector2.down;
            }

            hitX = Physics2D.Raycast(rayOriginX, dirX, dLength, layerMask);
            hitY = Physics2D.Raycast(rayOriginY + dirX * ledgerp, dirY, dLength, layerMask);
            dPosition = (Vector2)player.transform.position - rayOriginX;
            handPosition.x = hitX.point.x;
            handPosition.y = hitY.point.y;

            playerPosition = handPosition + dPosition;
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
            else if(p_bEndOfAnimation)
            {
                player.transform.position = playerTeleportPosition;
                player.fsm.Change(player.freeFall);
                return true;
            }


            // NOTE: 테스트 코드.
            else if(Input.GetKeyDown(KeyCode.Return))
            {
                p_bEndOfAnimation = true;
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