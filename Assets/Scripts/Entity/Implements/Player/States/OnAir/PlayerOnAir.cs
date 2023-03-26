using UnityEngine;

namespace Unchord
{
    public abstract class PlayerOnAir : PlayerState
    {
        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.skill00)
            {
                if(instance.axis.y < 0)
                    return Player.c_st_TAKE_DOWN;
                else
                    return Player.c_st_ATTACK_ON_AIR;
            }
            else if(instance.countLeft_JumpOnAir > 0 && instance.jumpDown)
                return Player.c_st_JUMP_ON_AIR;
            else if(instance.rushDown)
                return Player.c_st_DASH;
            else if(instance.senseData.bOnFloor)
                return Player.c_st_IDLE_SHORT;
            else if(instance.senseData.bOnDetectFloor)
                return MachineConstant.c_lt_PASS;
            else if(instance.axis.x != 0)
            {
                if(instance.senseData.bOnLedge)
                    return Player.c_st_CLIMB_LEDGE;
                else if(instance.senseData.bOnWallFront)
                    return Player.c_st_IDLE_WALL_FRONT;
            }

            return MachineConstant.c_lt_PASS;
        }
    }
}