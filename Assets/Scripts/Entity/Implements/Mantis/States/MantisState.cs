namespace Unchord
{
    public abstract class MantisState : MonsterState<Mantis>
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.senseData.OnFixedUpdate(instance);
            instance.aiCenter.localPosition = instance.aiCenterOffset;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(instance.health <= 0)
            {
                if(instance.phase == 0)
                    return Mantis.c_st_SHOUT;
                else if(instance.phase == 1)
                    return Mantis.c_st_DIE;
            }
            else if(instance.groggyValue >= 1.0f)
                return Mantis.c_st_GROGGY;
            else if(transit != MachineConstant.c_lt_PASS)
                return transit;

            return MachineConstant.c_lt_PASS;
        }
    }
}