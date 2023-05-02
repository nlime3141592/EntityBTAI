using UnityEngine;

namespace Unchord
{
    public class MantisDie : MantisOnFloor
    {
        public override int idConstant => Mantis.c_st_DIE;

        public override int Transit()
        {
            if(instance.aController.bEndOfAnimation)
                return MachineConstant.c_lt_END;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();

            instance.bEndOfEntity = true;
        }
    }
}