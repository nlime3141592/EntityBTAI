using UnityEngine;

namespace Unchord
{
    public class StaticObjectFreeFall : StaticObjectState
    {
        public override int idConstant => StaticObject.c_st_FREE_FALL;

        private float m_vy;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float dSpeed = (1.0f + instance.gravityPercent.finalValue / 100) * instance.gravity * Time.fixedDeltaTime;
            m_vy = Utilities.Max<float>(instance.speedMin_FreeFall, m_vy + dSpeed);
            instance.vm.SetVelocityY(m_vy);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            m_vy = Utilities.Max<float>(instance.speedMin_FreeFall, instance.vm.x);
            instance.vm.FreezePosition(false, false);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.senseData.datFloorL.bOnHit || instance.senseData.datFloorR.bOnHit)
                return StaticObject.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }
    }
}