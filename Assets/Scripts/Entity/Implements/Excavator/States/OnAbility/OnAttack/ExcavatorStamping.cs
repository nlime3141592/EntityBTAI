namespace Unchord
{
    public class ExcavatorStamping : ExcavatorAttack, ISkillEvent
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
            else if(instance.bEndOfAnimation)
                return Excavator.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }

        public void OnSkill(SkillModule _skModule)
        {
            System.Collections.Generic.List<Entity> targets = _skModule
                .Reset()
                .SenseColliders(instance.skillRange_stamping_01)
                .GetTargets();

            instance.skillRange_stamping_01.DebugSensor(UnityEngine.Color.cyan, 1.0f);

            foreach(Entity victim in targets)
            {
                float standardDamage = _skModule.GetStandardDamage(instance, victim);
                victim.ChangeHealth(-standardDamage);
            }
        }
    }
}