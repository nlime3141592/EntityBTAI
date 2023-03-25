using UnityEngine;

namespace Unchord
{
    public class ExcavatorOnFloor : ExcavatorState
    {
        public override int Transit()
        {
            int transit = base.Transit();
            
            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(!instance.senseData.bOnFloor)
                return Excavator.c_st_FREE_FALL;

            // NOTE: 테스트 입력 코드.
            else if(Input.GetKeyDown(KeyCode.F5))
                return Excavator.c_st_BREAK_GROUND;

            return MachineConstant.c_lt_PASS;
        }
    }
}