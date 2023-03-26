namespace Unchord
{
    public class ExcavatorStamping : ExcavatorAttack
    {
        public override void OnConstruct()
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

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.bUpdateAggroDirX = false;
            instance.bFixedLookDirByAxis.x = true;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Excavator.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }
    }
}