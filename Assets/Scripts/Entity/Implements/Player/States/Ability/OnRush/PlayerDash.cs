using UnityEngine;

namespace Unchord
{
    public class PlayerDash : PlayerRush
    {
        public override int idConstant => Player.c_st_DASH;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            --instance.countLeft_Dash;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.moveDir.x = 1;
            instance.moveDir.y = 0;

            float vx = instance.lookDir.fx * instance.moveDir.x * instance.speed_Dash;
            float vy = 0;

            instance.vm.SetVelocityXY(vx, vy);
        }

        public override bool CanTransit()
        {
            return base.CanTransit() && instance.countLeft_Dash > 0;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
            {
                Debug.Log("pass 1");
                return transit;
            }
            else if(instance.aController.bEndOfAnimation)
            {
                Debug.Log("pass 2");
                return Player.c_st_FREE_FALL;
            }
            else if(instance.countLeft_JumpOnAir > 0 && instance.jumpDown)
            {
                Debug.Log("pass 3");
                return Player.c_st_JUMP_ON_AIR;
            }

            Debug.Log("pass 4");
            return MachineConstant.c_lt_PASS;
        }
    }
}
