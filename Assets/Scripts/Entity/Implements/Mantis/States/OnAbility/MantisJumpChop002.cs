using System.Collections.Generic;

namespace Unchord
{
    // NOTE: 도약찍기 제 2상태, 착지 및 공격 동작
    public class MantisJumpChop002 : MantisAttack, ISkillEvent
    {
        public float baseDamage { get; private set; }

        public override int idConstant => Mantis.c_st_JUMP_CHOP_002;

        private List<Entity> m_targets;

        protected override void OnConstruct()
        {
            base.OnConstruct();

            baseDamage = 1.0f;

            m_targets = new List<Entity>(1);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.SetVelocityXY(0.0f, -0.1f);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            SensorUtilities.Bind(instance.transform, instance.skillRange_JumpChop002_01.transform);
            instance.skillRange_JumpChop002_01.OnUpdate();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bEndOfAnimation)
                return Mantis.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }

        public void OnSkill(SkillModule _skModule)
        {
instance.skillRange_JumpChop002_01.DebugSensor(UnityEngine.Color.cyan, 2.0f);
            List<Entity> targets = _skModule
                .Reset()
                .SenseColliders(instance.skillRange_JumpChop002_01)
                .GetTargets();

            foreach(Entity victim in targets)
            {
                float finalDamage = _skModule.GetStandardDamage(instance, victim);
                victim.ChangeHealth(-finalDamage);
            }
        }
    }
}