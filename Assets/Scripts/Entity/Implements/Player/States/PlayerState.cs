using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerState
    {
        protected _PlayerFSM fsm => player.fsm;

        protected readonly Player player;
        protected readonly PlayerData data;
        public readonly int id;
        public readonly string name;

        protected bool p_bEndOfAnimation;

        public PlayerState(Player player, PlayerData data, int id, string name)
        {
            this.player = player;
            this.data = data;
            this.id = id;
            this.name = name;
        }

        public virtual void OnStateBegin()
        {
            p_bEndOfAnimation = false;
        }

        public virtual void OnFixedUpdate()
        {

        }

        // 상태 변화가 일어나면 true, 그렇지 않으면 false를 반환하세요.
        public virtual bool OnUpdate()
        {
            m_CheckTerrains();
            return false;
        }

        public virtual void OnStateEnd()
        {

        }

        public virtual void OnAnimationEnd()
        {
            p_bEndOfAnimation = true;
        }

        private void m_CheckTerrains()
        {
            bool bDetectFloor = player.sensor.CheckFloor(player.originFloor.transform.position, data.detectLength);
            bool bHitFloor = player.sensor.CheckFloor(player.originFloor.transform.position, data.hitLength);
            bool bHitCeil = player.sensor.CheckCeil(player.originCeil.transform.position, data.hitLength);

            bool bHitWallFrontT = false;
            bool bHitWallFrontB = false;
            bool bHitWallBackT = false;
            bool bHitWallBackB = false;
            bool bDetectLedgeHorizontal = true;
            bool bDetectLedgeVertical = false;

            if(player.lookDir.x > 0)
            {
                bHitWallFrontT = player.sensor.CheckWallFront(player.originWallRT.position, data.hitLength, player.lookDir.x);
                bHitWallFrontB = player.sensor.CheckWallFront(player.originWallRB.position, data.hitLength, player.lookDir.x);
                bHitWallBackT = player.sensor.CheckWallBack(player.originWallLT.position, data.hitLength, player.lookDir.x);
                bHitWallBackB = player.sensor.CheckWallBack(player.originWallLB.position, data.hitLength, player.lookDir.x);

                bDetectLedgeHorizontal = player.sensor.CheckLedgeHorizontal(player.originLedgeRT.position, data.detectLength, player.lookDir.x);
                bDetectLedgeVertical = player.sensor.CheckLedgeVerticalDown(player.originLedgeRT.position + Vector3.right * data.ledgerp, data.detectLength * data.ledgeVerticalLengthWeight);
            }
            else if(player.lookDir.x < 0)
            {
                bHitWallBackT = player.sensor.CheckWallBack(player.originWallRT.position, data.hitLength, player.lookDir.x);
                bHitWallBackB = player.sensor.CheckWallBack(player.originWallRB.position, data.hitLength, player.lookDir.x);
                bHitWallFrontT = player.sensor.CheckWallFront(player.originWallLT.position, data.hitLength, player.lookDir.x);
                bHitWallFrontB = player.sensor.CheckWallFront(player.originWallLB.position, data.hitLength, player.lookDir.x);

                bDetectLedgeHorizontal = player.sensor.CheckLedgeHorizontal(player.originLedgeLT.position, data.detectLength, player.lookDir.x);
                bDetectLedgeVertical = player.sensor.CheckLedgeVerticalDown(player.originLedgeLT.position - Vector3.right * data.ledgerp, data.detectLength * data.ledgeVerticalLengthWeight);
            }

            player.bOnDetectFloor = bDetectFloor;
            player.bOnFloor = bHitFloor;
            player.bOnCeil = bHitCeil;
            player.bOnWallFrontT = bHitWallFrontT;
            player.bOnWallFrontB = bHitWallFrontB;
            player.bOnWallFront = bHitWallFrontT && bHitWallFrontB && bDetectLedgeHorizontal;
            player.bOnWallBackT = bHitWallBackT;
            player.bOnWallBackB = bHitWallBackB;
            player.bOnWallBack = bHitWallBackT && bHitWallBackB;
            player.bOnLedgeHorizontal = bDetectLedgeHorizontal;
            player.bOnLedgeVertical = bDetectLedgeVertical;
            player.bOnLedge = !bDetectLedgeHorizontal && bDetectLedgeVertical;
        }
    }
}