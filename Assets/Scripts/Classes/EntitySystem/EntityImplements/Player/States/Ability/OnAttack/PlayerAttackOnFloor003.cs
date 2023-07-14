using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class PlayerAttackOnFloor003 : PlayerAttackOnFloorBase, ISkillEvent
    {
        // TODO: 값 대입을 파일 처리할 수 있도록 할 것.
        public float baseDamage { get; private set; }
        public float speed_Step { get; private set; }

        private List<Entity> m_targets;

        public override int idConstant => Player.c_st_ATTACK_ON_FLOOR_003;

        protected override void OnConstruct()
        {
            base.OnConstruct();

            baseDamage = 1.25f;
            speed_Step = 2.0f;

            m_targets = new List<Entity>();
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.timerCoyote_AttackOnFloor.SetTimer(instance.timeCoyote_AttackOnFloor003);
            instance.stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_001;
        }

        public void OnSkill(SkillModule _skModule)
        {
            List<Entity> targets = _skModule
                .Reset()
                .SenseColliders(instance.skillRange_AttackOnFloor003_01)
                .GetTargets();

            instance.skillRange_AttackOnFloor003_01.DebugSensor(UnityEngine.Color.magenta, 2.0f);

            foreach(Entity victim in targets)
                _skModule.TakeStandardDamage(victim, 1.0f);
        }
    }
}