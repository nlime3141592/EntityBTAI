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
            fsm.leftAirJumpCount = data.maxAirJumpCount;
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.jumpDown)
            {
                fsm.Change(fsm.jumpOnWallFront);
                return true;
            }
            else if(fsm.bOnDetectFloor)
            {
                fsm.Change(fsm.freeFall);
                return true;
            }
            else if(player.axisInput.y < 0 && player.axisInput.x == 0)
            {
                fsm.Change(fsm.freeFall);
                return true;
            }
            else if(!fsm.bOnWallFront)
            {
                fsm.Change(fsm.freeFall);
                return true;
            }

            return false;
        }
    }
}