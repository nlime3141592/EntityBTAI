using UnityEngine;

namespace Unchord
{
    public class ExcavatorGroggy : ExcavatorOnFloor
    {
        public override int idConstant => Excavator.c_st_GROGGY;

        private float m_time_leftGroggy;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            m_time_leftGroggy = instance.time_groggy;
        }

        public override void OnFixedUpdate()
        {
            instance.vm.SetVelocityY(-1.0f);
        }

        public override void OnUpdate()
        {
            if(m_time_leftGroggy > 0)
                m_time_leftGroggy -= Time.deltaTime;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_time_leftGroggy <= 0 && instance.aController.bEndOfAnimation)
                return Excavator.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }
    }
}