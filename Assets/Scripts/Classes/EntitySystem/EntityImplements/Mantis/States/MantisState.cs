using UnityEngine;

namespace Unchord
{
    public abstract class MantisState : MonsterState<Mantis>,
    IEntityAggressionEvents
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.senseData.OnFixedUpdate(instance);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(instance.health <= 0)
            {
                if(instance.phase == 0)
                    return Mantis.c_st_PHASE_SHOUT;
                else if(instance.phase == 1)
                    return Mantis.c_st_DIE;
            }
            else if(instance.groggyValue >= instance.maxGroggyValue.finalValue)
                return Mantis.c_st_GROGGY;
            else if(transit != MachineConstant.c_lt_PASS)
                return transit;

            return MachineConstant.c_lt_PASS;
        }

        public virtual void OnAggroBegin(SEH_EntityAggression _aggModule)
        {
            instance.bAggro = true;
        }

        public virtual void OnAggroEnd(SEH_EntityAggression _aggModule)
        {
            instance.bAggro = false;
        }

        public virtual void OnAggressive(SEH_EntityAggression _aggModule)
        {
            instance.aggroTargets = _aggModule.targets;
        }
    }
}