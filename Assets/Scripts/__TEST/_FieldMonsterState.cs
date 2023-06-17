namespace Unchord
{
    public abstract class _FieldMonsterState : EntityState<_FieldMonster>,
    IEntityAggressionEvents
    {
        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.nextTransit != -1)
                return instance.nextTransit;
            
            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.nextTransit = -1;
            instance.reservoir.Clear();
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

    public class _FieldMonsterWalk : _FieldMonsterState
    {
        public override int idConstant => _FieldMonster.c_st_WALK;

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();

            if(instance.bAggro)
            {
                instance.reservoir.AddStream(instance.prng, instance.weight1[_FieldMonster.c_st_WALK], _FieldMonster.c_st_WALK);
                instance.reservoir.AddStream(instance.prng, instance.weight1[_FieldMonster.c_st_FUNC_AGGRESSIVE], _FieldMonster.c_st_FUNC_AGGRESSIVE);
                instance.nextTransit = instance.reservoir.idxSaved;
            }
            else
            {
                instance.reservoir.AddStream(instance.prng, instance.weight0[_FieldMonster.c_st_IDLE], _FieldMonster.c_st_IDLE);
                instance.reservoir.AddStream(instance.prng, instance.weight0[_FieldMonster.c_st_WALK], _FieldMonster.c_st_WALK);
                // instance.reservoir.AddStream(instance.prng, instance.weight0[_FieldMonster.c_st_FUNC_NON_AGGRESSIVE], _FieldMonster.c_st_FUNC_NON_AGGRESSIVE);
                instance.nextTransit = instance.reservoir.idxSaved;
            }
        }
    }

    public class _FieldMonsterIdle : _FieldMonsterState
    {
        public override int idConstant => _FieldMonster.c_st_IDLE;

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();

            if(instance.bAggro)
            {
                instance.reservoir.AddStream(instance.prng, instance.weight1[_FieldMonster.c_st_WALK], _FieldMonster.c_st_WALK);
                instance.reservoir.AddStream(instance.prng, instance.weight1[_FieldMonster.c_st_FUNC_AGGRESSIVE], _FieldMonster.c_st_FUNC_AGGRESSIVE);
                instance.nextTransit = instance.reservoir.idxSaved;
            }
            else
            {
                instance.reservoir.AddStream(instance.prng, instance.weight0[_FieldMonster.c_st_WALK], _FieldMonster.c_st_WALK);
                instance.reservoir.AddStream(instance.prng, instance.weight0[_FieldMonster.c_st_IDLE], _FieldMonster.c_st_IDLE);
                // instance.reservoir.AddStream(instance.prng, instance.weight0[_FieldMonster.c_st_FUNC_NON_AGGRESSIVE], _FieldMonster.c_st_FUNC_NON_AGGRESSIVE);
                instance.nextTransit = instance.reservoir.idxSaved;
            }
        }
    }

    public class _FieldMonsterAttack : _FieldMonsterState
    {
        public override int idConstant => _FieldMonster.c_st_FUNC_AGGRESSIVE;

        public override void OnAnimationEnd()
        {
            base.OnAnimationEnd();

            if(instance.bAggro)
            {
                instance.reservoir.AddStream(instance.prng, instance.weight1[_FieldMonster.c_st_WALK], _FieldMonster.c_st_WALK);
                instance.reservoir.AddStream(instance.prng, instance.weight1[_FieldMonster.c_st_FUNC_AGGRESSIVE], _FieldMonster.c_st_FUNC_AGGRESSIVE);
                instance.nextTransit = instance.reservoir.idxSaved;
            }
            else
            {
                instance.reservoir.AddStream(instance.prng, instance.weight0[_FieldMonster.c_st_WALK], _FieldMonster.c_st_WALK);
                instance.reservoir.AddStream(instance.prng, instance.weight0[_FieldMonster.c_st_IDLE], _FieldMonster.c_st_IDLE);
                // instance.reservoir.AddStream(instance.prng, instance.weight0[_FieldMonster.c_st_FUNC_NON_AGGRESSIVE], _FieldMonster.c_st_FUNC_NON_AGGRESSIVE);
                instance.nextTransit = instance.reservoir.idxSaved;
            }
        }
    }
}