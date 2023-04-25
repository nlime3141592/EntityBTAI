namespace Unchord
{
    public class MantisKnifeGrinding : MantisOnFloor
    {
        public override int idConstant => Mantis.c_st_KNIFE_GRINDING;

        public override int Transit()
        {
            if(!instance.aController.bEndOfAnimation)
                return MachineConstant.c_lt_CONTINUE;
            else if(instance.monsterPhase == 1 && instance.health <= 0)
                return Mantis.c_st_IDLE;

            int transit = base.Transit();

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            if(instance.monsterPhase == 1 && instance.health <= 0)
            {
                instance.monsterPhase = 2; // 2페이즈로 진입합니다.
                instance.SetHealth(instance.maxHealth.finalValue);
            }
        }
    }
}