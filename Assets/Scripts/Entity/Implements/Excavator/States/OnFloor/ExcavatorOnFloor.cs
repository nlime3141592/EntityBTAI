using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorOnFloor : ExcavatorState
    {
        public ExcavatorOnFloor(Excavator _instance)
        : base(_instance)
        {
            
        }

        public override int Transit()
        {
            int transit = base.Transit();
            
            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(!excavator.senseData.bOnFloor)
                return ExcavatorFsm.c_st_FREE_FALL;

            // NOTE: 테스트 입력 코드.
            else if(Input.GetKeyDown(KeyCode.F5))
                return ExcavatorFsm.c_st_BREAK_GROUND;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}