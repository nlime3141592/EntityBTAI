using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerOnWallFront : PlayerState
    {
        public PlayerOnWallFront(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            player.leftAirJumpCount = data.maxAirJumpCount;
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
            else if(player.senseData.bOnDetectFloor)
            {
                fsm.Change(fsm.freeFall);
                return true;
            }
            else if(player.axisInput.y < 0 && player.axisInput.x == 0)
            {
                fsm.Change(fsm.freeFall);
                return true;
            }
            else if(!player.senseData.bOnWallFront)
            {
                fsm.Change(fsm.freeFall);
                return true;
            }

            return false;
        }
    }
}