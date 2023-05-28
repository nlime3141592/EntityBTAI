using System.Collections.Generic;

namespace Unchord
{
    public class MantisBackSlice : MantisAttack, ISkillEvent
    {
        public float baseDamage { get; private set; }

        public override int idConstant => Mantis.c_st_BACK_SLICE;

        private List<Entity> m_targets;

        protected override void OnConstruct()
        {
            base.OnConstruct();

            baseDamage = 1.0f;

            m_targets = new List<Entity>(1);
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
            if(instance.lookDir.x == Direction.Positive)
                instance.lookDir.x = Direction.Negative;
            else
                instance.lookDir.x = Direction.Positive;

            FixedUpdateRotation();

            List<Entity> targets = _skModule
                .Reset()
                .SenseColliders(instance.skillRange_BackSlice_01)
                .GetTargets();

            instance.skillRange_BackSlice_01.DebugSensor(UnityEngine.Color.cyan, 2.0f);

            foreach(Entity victim in targets)
            {
                if(victim is Player && _skModule.TryGroggy(victim))
                    continue;
                else
                    _skModule.TakeStandardDamage(victim, 1.0f);
            }
        }
    }
}