namespace Unchord
{
    public class ExcavatorStamping : ExcavatorAttack, IBattleState
    {
        public override int idConstant => Excavator.c_st_STAMPING;
/*
        protected override void OnConstruct()
        {
            base.OnConstruct();

            base.attackRange = new LTRB()
            {
                left = 0.0f,
                top = 0.0f,
                right = 10.5f,
                bottom = 8.0f
            };
            base.targetCount = 12;
            base.baseDamage = 1.0f;
        }
*/
        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Excavator.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }

        public void OnTriggerBattleState(BattleModule _btModule)
        {
            instance.skillRange_stamping_01.Sense(in instance.sensorBuffer, _btModule.tags, _btModule.mask);
        }
    }
}