using System.Collections.Generic;

namespace Unchord
{
    public class PlayerAttackOnAir002 : PlayerAttackOnAirBase, ISkillEvent
    {
        public override int idConstant => Player.c_st_ATTACK_ON_AIR_002;

        protected override void OnConstruct()
        {
            base.OnConstruct();

            base.baseDamage = 1.0f;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            base.speed_Hop = instance.speed_AttackOnAir002;

            instance.stateNext_AttackOnAir = Player.c_st_ATTACK_ON_AIR_001;
            instance.timerCoyote_AttackOnAir.SetTimer(instance.timeCoyote_AttackOnAir002);
        }

        public void OnSkill(SkillModule _skModule)
        {
            List<Entity> targets = _skModule
                .Reset()
                .SenseColliders(instance.skillRange_AttackOnAir002_01)
                .GetTargets();

            instance.skillRange_AttackOnAir002_01.DebugSensor(UnityEngine.Color.magenta, 2.0f);

            foreach(Entity victim in targets)
                _skModule.TakeStandardDamage(victim, 1.0f);
        }
    }
}