using UnityEngine;

namespace Unchord
{
    // TODO: Rigidbody2D의 물리 연산 모드를 Continuous로 설정해야 함.

    [RequireComponent(typeof(SEH_OnTriggerEnter2D))]
    [RequireComponent(typeof(SkillModule))]
    [AddComponentMenu("Unchord System/Entity Components/Excavator Wave")]
    public class ExcavatorWave : Entity
    {
        public const int c_st_IDLE = 0;
        public const int c_st_SHAKE = 1;

        public int waveStep = 15;
        public bool bInstanceReady = false;

        public ExcavatorWaveTerrainSensor senseData;

        public override void OnAwakeEntity()
        {
            base.OnAwakeEntity();

            senseData = new ExcavatorWaveTerrainSensor();
        }

        public override IStateMachineBase InitStateMachine()
        {
            StateMachine<ExcavatorWave> machine = new StateMachine<ExcavatorWave>(2);
            machine.instance = this;

            machine.Add(new ExcavatorWaveIdle());
            machine.Add(new ExcavatorWaveShake());

            machine.Begin(ExcavatorWave.c_st_IDLE);
            return machine;
        }
    }
}