using UnityEngine;

namespace Unchord
{
    // NOTE: 애니메이션 상태-조준
    public class ExcavatorAnchoring002 : ExcavatorAttack
    {
        public override int idConstant => Excavator.c_st_ANCHORING_002;

        private float m_time_leftAnchor;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.rightArm.nextState = ExcavatorRightArm.c_st_MOVE;
            m_time_leftAnchor = instance.time_anchor;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(m_time_leftAnchor > 0)
                m_time_leftAnchor -= Time.deltaTime;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_time_leftAnchor <= 0)
                return Excavator.c_st_ANCHORING_003;

            return MachineConstant.c_lt_PASS;
        }
    }
}