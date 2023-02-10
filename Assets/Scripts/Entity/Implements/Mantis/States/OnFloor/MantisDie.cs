using UnityEngine;

namespace UnchordMetroidvania
{
    public class MantisDie : MantisOnFloor
    {
        public MantisDie(Mantis instance)
        : base(instance)
        {

        }

        public override int Transit()
        {
            if(mantis.aController.bEndOfAnimation)
                return FiniteStateMachine.c_st_MACHINE_HALT;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            mantis.bEndOfEntity = true;
        }
    }
}