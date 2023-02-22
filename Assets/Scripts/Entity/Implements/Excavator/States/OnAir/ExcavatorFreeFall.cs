using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorFreeFall : ExcavatorOnAir
    {
        public ExcavatorFreeFall(Excavator _instance)
        : base(_instance)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vy = excavator.vm.y;
            float dV = data.gravity * Time.fixedDeltaTime;

            vy += dV;
            if(vy < data.minFreeFallSpeed)
                vy = data.minFreeFallSpeed;

            excavator.vm.SetVelocityXY(0.0f, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(excavator.senseData.bOnFloor)
                return ExcavatorFsm.c_st_BASIC_LANDING;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}