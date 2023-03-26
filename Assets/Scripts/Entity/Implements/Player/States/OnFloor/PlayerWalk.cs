using UnityEngine;

namespace Unchord
{
    public class PlayerWalk : PlayerMove
    {
        public override void OnMachineBegin(Player _instance, int _id)
        {
            base.OnMachineBegin(_instance, _id);

            _instance.stateMap.Add(Player.c_st_WALK, _id);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = instance.axis.x * instance.moveDir.x * instance.speed_Walk;
            float vy = instance.axis.x * instance.moveDir.y * instance.speed_Walk - 0.1f;

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