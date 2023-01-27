using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerOnAir : PlayerState
    {
        public PlayerOnAir(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.axisInput.y < 0 && player.jumpDown)
            {
                fsm.Change(fsm.takeDown);
                return true;
            }
            else if(player.skill00 && fsm.attackOnAir.CanAttack())
            {
                fsm.Change(fsm.attackOnAir);
                return true;
            }
            else if(player.leftAirJumpCount > 0 && player.jumpDown)
            {
                fsm.Change(fsm.jumpOnAir);
                return true;
            }
            else if(player.rushDown)
            {
                fsm.Change(fsm.dash);
                return true;
            }
            else if(player.senseData.bOnFloor)
            {
                fsm.Change(fsm.idleShort);
                return true;
            }
            else if(player.senseData.bOnDetectFloor)
            {
                return false;
            }
            else if(player.axisInput.x != 0)
            {
                if(player.senseData.bOnLedge)
                {
                    fsm.Change(fsm.climbLedge);
                    return true;
                }
                else if(player.senseData.bOnWallFront)
                {
                    fsm.Change(fsm.idleWallFront);
                    return true;
                }
            }
            return false;
        }
    }
}