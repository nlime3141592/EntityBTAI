using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class ExcavatorState : MonsterState<Excavator>
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.senseData.UpdateData(instance);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.health <= 0.0f)
            {
                if(++instance.monsterPhase <= 3)
                    return Excavator.c_st_BREAK_GROUND;
                else if(fsm.current != Excavator.c_st_DIE)
                    return Excavator.c_st_DIE;
            }
            else if(instance.groggyValue >= 1.0f)
                return Excavator.c_st_GROGGY;

            return MachineConstant.c_lt_PASS;
        }
    }
}