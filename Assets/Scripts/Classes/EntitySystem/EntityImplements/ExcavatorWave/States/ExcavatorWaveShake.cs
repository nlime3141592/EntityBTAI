using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class ExcavatorWaveShake : ExcavatorWaveState, ISkillEvent
    {
        public override int idConstant => ExcavatorWave.c_st_SHAKE;

        public float baseDamage { get; private set; }
        public int targetCount { get; private set; }
        private List<Entity> m_targets;

        protected override void OnConstruct()
        {
            base.OnConstruct();

            m_targets = new List<Entity>(targetCount);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(true, true);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bEndOfAnimation)
                return MachineConstant.c_st_MACHINE_OFF;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnActionBegin()
        {
            base.OnActionBegin();

            m_SpreadWave();
        }

        public void OnSkill(SkillModule _skModule)
        {
            // 1. 데미지 가함
        }

        private void m_SpreadWave()
        {
            if(instance.waveStep <= 0)
                return;

            ExcavatorWave waveL = GameObject.Instantiate<ExcavatorWave>(instance);
            ExcavatorWave waveR = GameObject.Instantiate<ExcavatorWave>(instance);

            --waveL.waveStep;
            --waveR.waveStep;

            waveL.bInstanceReady = true;
            waveR.bInstanceReady = true;
        }
    }
}