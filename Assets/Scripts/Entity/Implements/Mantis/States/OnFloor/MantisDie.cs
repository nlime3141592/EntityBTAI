using UnityEngine;

namespace Unchord
{
    public class MantisDie : MantisOnFloor
    {
        public override int Transit()
        {
            if(instance.aController.bEndOfAnimation)
                return MachineConstant.c_lt_END;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnMachineEnd()
        {
            base.OnMachineEnd();

            instance.bEndOfEntity = true;
        }
    }
}