using UnityEngine;

namespace Unchord
{
    // NOTE: 애니메이션 상태-발사
    public class ExcavatorAnchoring003 : ExcavatorAttack
    {
        public override int idConstant => Excavator.c_st_ANCHORING_003;

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;

            return MachineConstant.c_lt_PASS;
        }
    }
}