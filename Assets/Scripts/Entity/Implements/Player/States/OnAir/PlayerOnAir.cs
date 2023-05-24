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
            else if(instance.iManager.active000)
            {
                if(instance.iManager.iy < 0)
                    return Player.c_st_TAKE_DOWN_001;
                else
                    return instance.stateNext_AttackOnAir;
            }
            else if(instance.countLeft_JumpOnAir > 0 && instance.iManager.jumpDown)
                return Player.c_st_JUMP_ON_AIR;
            else if(instance.iManager.rushDown)
                return Player.c_st_DASH;
            else if(bCanLandOnFloor())
                return Player.c_st_IDLE_SHORT;
            else if(instance.senseData.datFloor.bOnDetected)
                return MachineConstant.c_lt_PASS;
            else if(instance.iManager.ix != 0)
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