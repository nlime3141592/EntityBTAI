using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public abstract class ExcavatorState : MonsterState<Excavator>
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.senseData.UpdateOrigins(instance);
            instance.senseData.UpdateData(instance);
            instance.aiCenter.localPosition = instance.aiCenterOffset;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            // NOTE: test input code
            float ixn = Input.GetKeyDown(KeyCode.J) ? -1 : 0;
            float ixp = Input.GetKeyDown(KeyCode.L) ? 1 : 0;
            float iyn = Input.GetKeyDown(KeyCode.I) ? -1 : 0;
            float iyp = Input.GetKeyDown(KeyCode.K) ? 1 : 0;
            float ix = ixn + ixp;
            float iy = iyn + iyp;
            // axisInput.x = ix;
            // axisInput.y = iy;

            instance.arm.yAngle = instance.transform.eulerAngles.y;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.health <= 0.0f)
            {
                if(instance.phase < 2)
                    return Excavator.c_st_BREAK_GROUND;
                else
                    return Excavator.c_st_DIE;
            }
            else if(instance.groggyValue >= 1.0f)
                return Excavator.c_st_GROGGY;

            return MachineConstant.c_lt_PASS;
        }
    }
}