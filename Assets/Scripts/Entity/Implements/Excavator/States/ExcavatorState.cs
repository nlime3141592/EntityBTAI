using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorState : MonsterState<Excavator>
    {
        protected Excavator excavator => instance;
        protected ExcavatorFsm fsm => instance.fsm;
        protected ExcavatorData data => instance.data;

        public ExcavatorState(Excavator _instance)
        : base(_instance)
        {
            
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            excavator.senseData.UpdateData(excavator);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}