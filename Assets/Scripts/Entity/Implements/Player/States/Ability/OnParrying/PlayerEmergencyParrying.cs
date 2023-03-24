using UnityEngine;

namespace Unchord
{
    public class PlayerEmergencyParrying : PlayerParrying
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.vm.SetVelocityXY(0.0f, -1.0f);
        }
    }
}