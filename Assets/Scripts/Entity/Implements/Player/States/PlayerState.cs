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

        public PlayerState(Player player, PlayerData data, int id, string name)
        {
            this.player = player;
            this.data = data;
            this.id = id;
            this.name = name;
        }

        public virtual void OnStateBegin()
        {

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
            
        }

        private void m_CheckTerrains()
        {
            bool bDetectFloor = player.sensor.CheckFloor(player.originFloor.transform.position, 0.5f);
            bool bHitFloor = player.sensor.CheckFloor(player.originFloor.transform.position, 0.04f);
            bool bHitCeil = player.sensor.CheckCeil(player.originCeil.transform.position, 0.04f);

            bool bHitWallFrontT = false;
            bool bHitWallFrontB = false;
            bool bHitWallBackT = false;
            bool bHitWallBackB = false;
            bool bDetectLedgeHorizontal = true;
            bool bDetectLedgeVertical = false;

            if(player.lookDir.x > 0)
            {
                bHitWallFrontT = player.sensor.CheckWallFront(player.originWallRT.transform.position, 0.06f, player.lookDir.x);
                bHitWallFrontB = player.sensor.CheckWallFront(player.originWallRB.transform.position, 0.06f, player.lookDir.x);
                bHitWallBackT = player.sensor.CheckWallBack(player.originWallLT.transform.position, 0.06f, player.lookDir.x);
                bHitWallBackB = player.sensor.CheckWallBack(player.originWallLB.transform.position, 0.06f, player.lookDir.x);

                bDetectLedgeHorizontal = player.sensor.CheckLedgeHorizontal(player.originLedgeRT.transform.position, 0.5f, player.lookDir.x);
                bDetectLedgeVertical = player.sensor.CheckLedgeVerticalDown(player.originLedgeRT.transform.position + Vector3.right * 0.1f, 0.3f);
            }
            else if(player.lookDir.x < 0)
            {
                bHitWallBackT = player.sensor.CheckWallBack(player.originWallRT.transform.position, 0.06f, player.lookDir.x);
                bHitWallBackB = player.sensor.CheckWallBack(player.originWallRB.transform.position, 0.06f, player.lookDir.x);
                bHitWallFrontT = player.sensor.CheckWallFront(player.originWallLT.transform.position, 0.06f, player.lookDir.x);
                bHitWallFrontB = player.sensor.CheckWallFront(player.originWallLB.transform.position, 0.06f, player.lookDir.x);

                bDetectLedgeHorizontal = player.sensor.CheckLedgeHorizontal(player.originLedgeLT.transform.position, 0.5f, player.lookDir.x);
                bDetectLedgeVertical = player.sensor.CheckLedgeVerticalDown(player.originLedgeLT.transform.position - Vector3.right * 0.1f, 0.3f);
            }

            player.bOnDetectFloor = bDetectFloor;
            player.bOnFloor = bHitFloor;
            player.bOnCeil = bHitCeil;
            player.bOnWallFrontT = bHitWallFrontT;
            player.bOnWallFrontB = bHitWallFrontB;
            player.bOnWallFront = bHitWallFrontT && bHitWallFrontB;
            player.bOnWallBackT = bHitWallBackT;
            player.bOnWallBackB = bHitWallBackB;
            player.bOnWallBack = bHitWallBackT && bHitWallBackB;
            player.bOnLedgeHorizontal = bDetectLedgeHorizontal;
            player.bOnLedgeVertical = bDetectLedgeVertical;
            player.bOnLedge = !bDetectLedgeHorizontal && bDetectLedgeVertical;
        }
    }
}