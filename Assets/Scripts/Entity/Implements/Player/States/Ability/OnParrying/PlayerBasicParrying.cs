using UnityEngine;

namespace Unchord
{
    public class PlayerBasicParrying : PlayerParrying
    {
        public override void OnMachineBegin(Player _instance, int _id)
        {
            base.OnMachineBegin(_instance, _id);

            _instance.stateMap.Add(Player.c_st_BASIC_PARRYING, _id);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.vm.SetVelocityXY(0.0f, -1.0f);
        }
    }
}