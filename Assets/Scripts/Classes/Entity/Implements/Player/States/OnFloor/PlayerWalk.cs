using UnityEngine;

namespace Unchord
{
    public class PlayerWalk : PlayerMove
    {
        public override int idConstant => Player.c_st_WALK;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = instance.iManager.ix * instance.moveDir.x * instance.speed_Walk;
            float vy = instance.iManager.ix * instance.moveDir.y * instance.speed_Walk - 0.1f;

            instance.vm.SetVelocityXY(vx, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bIsRun)
                return Player.c_st_RUN;

            return MachineConstant.c_lt_PASS;
        }
    }
}