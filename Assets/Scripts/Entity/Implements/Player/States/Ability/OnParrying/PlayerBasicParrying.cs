using UnityEngine;

namespace Unchord
{
    public class PlayerBasicParrying : PlayerParrying
    {
        public override int idConstant => Player.c_st_BASIC_PARRYING;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.vm.SetVelocityXY(0.0f, -1.0f);
        }
    }
}