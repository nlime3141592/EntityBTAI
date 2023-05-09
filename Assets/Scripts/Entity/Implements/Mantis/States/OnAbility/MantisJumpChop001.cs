using UnityEngine;

namespace Unchord
{
    // NOTE: 도약찍기 제 1상태, 점프 동작
    public class MantisJumpChop001 : MantisAttack
    {
        public override int idConstant => Mantis.c_st_JUMP_CHOP_001;

        private float m_vx;
        private float m_vy;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(!instance.aController.bBeginOfAction)
                return;

            instance.vm.SetVelocityXY(m_vx, m_vy);

            if(m_vy > 0)
                m_vy -= instance.force_jumpChopY * Time.fixedDeltaTime;
            else
                m_vy -= instance.force_jumpChopY * instance.force_jumpChopY * Time.fixedDeltaTime;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            m_vx = instance.speed_jumpChopX * instance.lookDir.fx;
            m_vy = instance.speed_jumpChopY;

            instance.vm.FreezePosition(false, false);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_vy <= 0 && (instance.senseData.datFloorBack.bOnHit || instance.senseData.datFloorFront.bOnHit))
                return Mantis.c_st_JUMP_CHOP_002;

            return MachineConstant.c_lt_PASS;
        }
    }
}