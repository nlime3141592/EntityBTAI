using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorSleep : ExcavatorState
    {
        public ExcavatorSleep(Excavator _instance)
        : base(_instance)
        {
            
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            fsm.mode = 1;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(excavator.bAggro)
                return ExcavatorFsm.c_st_WAKE_UP;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}