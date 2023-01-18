using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _PlayerOnAir : PlayerState
    {
        public _PlayerOnAir(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
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
            else if(fsm.leftAirJumpCount > 0 && player.jumpDown)
            {
                fsm.Change(fsm.jumpOnAir);
                return true;
            }
            else if(player.rushDown)
            {
                fsm.Change(fsm.dash);
                return true;
            }
            else if(fsm.bOnFloor)
            {
                fsm.Change(fsm.idleShort);
                return true;
            }
            else if(fsm.bOnDetectFloor)
            {
                return false;
            }
            else if(player.axisInput.x != 0)
            {
                if(fsm.bOnLedge)
                {
                    fsm.Change(fsm.climbLedge);
                    return true;
                }
                else if(fsm.bOnWallFront)
                {
                    fsm.Change(fsm.idleWallFront);
                    return true;
                }
            }
            return false;
        }
    }
}