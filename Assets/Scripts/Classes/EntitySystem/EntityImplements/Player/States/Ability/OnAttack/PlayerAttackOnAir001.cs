using System.Collections.Generic;

namespace Unchord
{
    public class PlayerAttackOnAir001 : PlayerAttackOnAirBase, ISkillEvent
    {
        public override int idConstant => Player.c_st_ATTACK_ON_AIR_001;

        protected override void OnConstruct()
        {
            base.OnConstruct();

            base.baseDamage = 1.0f;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            base.speed_Hop = instance.speed_AttackOnAir001;

            instance.stateNext_AttackOnAir = Player.c_st_ATTACK_ON_AIR_002;
            --instance.countLeft_AttackOnAir;
            instance.timerCoyote_AttackOnAir.SetTimer(instance.timeCoyote_AttackOnAir001);
        }

        public override bool CanTransit()
        {
            if(!base.CanTransit())
                return false;
            
            return instance.countLeft_AttackOnAir > 0;
        }

        public void OnSkill(SkillModule _skModule)
        {
            List<Entity> targets = _skModule
                .Reset()
                .SenseColliders(instance.skillRange_AttackOnAir001_01)
                .GetTargets();

            instance.skillRange_AttackOnAir001_01.DebugSensor(UnityEngine.Color.magenta, 2.0f);

            foreach(Entity victim in targets)
                _skModule.TakeStandardDamage(victim, 1.0f);
        }
    }
}