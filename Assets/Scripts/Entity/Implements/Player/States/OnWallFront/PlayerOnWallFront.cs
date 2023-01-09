using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerOnWallFront : PlayerState
    {
        public PlayerOnWallFront(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            player.leftAirJumpCount = data.maxAirJumpCount;
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.jumpDown)
            {
                player.fsm.Change(player.jumpOnWallFront);
                return true;
            }
            else if(player.bOnDetectFloor)
            {
                player.fsm.Change(player.freeFall);
                return true;
            }
            else if(player.axisInput.y < 0 && player.axisInput.x == 0)
            {
                player.fsm.Change(player.freeFall);
                return true;
            }
            else if(!player.bOnWallFront)
            {
                player.fsm.Change(player.freeFall);
                return true;
            }

            return false;
        }
    }
}