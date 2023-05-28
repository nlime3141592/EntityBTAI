using UnityEngine;

namespace Unchord
{
    // NOTE: 애니메이션 상태-자세 준비
    public class ExcavatorAnchoring001 : ExcavatorAttack
    {
        // public override int idConstant => Excavator.c_st_ANCHORING_001;
        public override int idConstant => Excavator.c_st_ANCHORING;

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bEndOfAnimation)
                return Excavator.c_st_ANCHORING_002;

            return MachineConstant.c_lt_PASS;
        }
    }
}