using UnityEngine;

namespace Unchord
{
    public abstract class PlayerMove : PlayerOnFloor
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.MeltPositionX();
            instance.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            // instance.senseData.UpdateMoveDir(instance);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.iManager.jumpDown)
                return Player.c_st_JUMP_ON_FLOOR;
            else if(instance.iManager.iy > 0)
                return Player.c_st_HEAD_UP;
            else if(instance.iManager.iy < 0)
                return Player.c_st_SIT;
            else if(instance.iManager.ix == 0)
                return Player.c_st_IDLE_SHORT;

            return MachineConstant.c_lt_PASS;
        }
    }
}