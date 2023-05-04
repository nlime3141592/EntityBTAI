using UnityEngine;

namespace Unchord
{
    // TODO: Rigidbody2D의 물리 연산 모드를 Continuous로 설정해야 함.

    [RequireComponent(typeof(StateEventTriggerOnTriggerEnter2D))]
    [RequireComponent(typeof(BattleModule))]
    public class ExcavatorWave : Entity
    {
        public const int c_st_IDLE = 0;
        public const int c_st_SHAKE = 1;

        public int waveStep = 15;
        public bool bInstanceReady = false;

        public ExcavatorWaveTerrainSensor senseData;

        protected override void InitComponents()
        {
            base.InitComponents();

            senseData = new ExcavatorWaveTerrainSensor();
        }

        protected override IStateMachineBase InitStateMachine()
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