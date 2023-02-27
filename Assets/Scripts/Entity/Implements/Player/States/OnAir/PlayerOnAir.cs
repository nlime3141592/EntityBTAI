using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerOnAir : PlayerState
    {
        public PlayerOnAir(Player _player)
        : base(_player)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.skill00)
            {
                if(player.axisInput.y < 0)
                    return PlayerFsm.c_st_TAKE_DOWN;
                else
                    return PlayerFsm.c_st_ATTACK_ON_AIR;
            }
            else if(player.leftAirJumpCount > 0 && player.jumpDown)
                return PlayerFsm.c_st_JUMP_ON_AIR;
            else if(player.rushDown)
                return PlayerFsm.c_st_DASH;
            else if(player.senseData.bOnFloor)
                return PlayerFsm.c_st_IDLE_SHORT;
            else if(player.senseData.bOnDetectFloor)
                return FiniteStateMachine.c_st_BASE_IGNORE;
            else if(player.axisInput.x != 0)
            {
                if(player.senseData.bOnLedge)
                    return PlayerFsm.c_st_CLIMB_LEDGE;
                else if(player.senseData.bOnWallFront)
                    return PlayerFsm.c_st_IDLE_WALL_FRONT;
            }

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}