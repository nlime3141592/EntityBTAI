using UnityEngine;

namespace Unchord
{
    // NOTE: 애니메이션 상태-회수
    public class ExcavatorAnchoring004 : ExcavatorAttack
    {
        public override int idConstant => Excavator.c_st_ANCHORING_004;

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;

            return MachineConstant.c_lt_PASS;
        }
    }
}