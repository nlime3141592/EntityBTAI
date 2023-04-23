using UnityEngine;

namespace Unchord
{
    public class SandBagDie : SandBagState
    {
        public override void OnConstruct()
        {
            base.OnConstruct();

            idFixed = SandBag.c_st_DIE;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(true, false);
            GameObject.Destroy(instance.gameObject);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.vm.SetVelocityY(-10.0f);
        }
    }
}