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
            else if(player.skill00 && player.attackOnAir.CanAttack())
            {
                player.fsm.Change(player.attackOnAir);
                return true;
            }
            else if(player.leftAirJumpCount > 0 && player.jumpDown)
            {
                player.fsm.Change(player.jumpOnAir);
                return true;
            }
            else if(player.rushDown)
            {
                player.fsm.Change(player.dash);
                return true;
            }
            else if(player.bOnFloor)
            {
                player.fsm.Change(player.idleShort);
                return true;
            }
            else if(player.bOnDetectFloor)
            {
                return false;
            }
            else if(player.axisInput.x != 0)
            {
                if(player.bOnLedge)
                {
                    player.fsm.Change(player.climbLedge);
                    return true;
                }
                else if(player.bOnWallFront)
                {
                    player.fsm.Change(player.idleWallFront);
                    return true;
                }
            }
            return false;
        }
    }
}