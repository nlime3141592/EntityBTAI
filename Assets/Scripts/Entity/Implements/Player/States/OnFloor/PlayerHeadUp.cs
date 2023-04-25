using UnityEngine;

namespace Unchord
{
    public class PlayerHeadUp : PlayerStand
    {
        public override int idConstant => Player.c_st_HEAD_UP;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.offset_StandCamera = Vector2.up;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.jumpDown)
                return Player.c_st_JUMP_ON_FLOOR;

            return MachineConstant.c_lt_PASS;
        }
    }
}