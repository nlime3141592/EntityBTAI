using System;

namespace UnchordMetroidvania
{
    public class FiniteState : FiniteState<object>
    {
        public FiniteState(object _instance)
        : base(_instance)
        {
            
        }

        protected bool CheckBaseTransit(int _state, int _capacity)
        {
            switch(_state)
            {
                case FiniteStateMachine.c_st_MACHINE_HALT:
                case FiniteStateMachine.c_st_STATE_CONTINUE:
                    return true;
                case FiniteStateMachine.c_st_BASE_IGNORE:
                    return false;
                default:
                    break;
            }

            if(_state < 0 || _state >= _capacity)
                throw new StateTransitException("Check your transit function.");
            return true;
        }
    }
}