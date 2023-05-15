using UnityEngine;

namespace Unchord
{
    public class ExcavatorShockWave : ExcavatorAttack, IBattleState
    {
        public override int idConstant => Excavator.c_st_SHOCK_WAVE;

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bEndOfAnimation)
                return Excavator.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }

        public void OnTriggerBattleState(BattleModule _btModule)
        {
            // TODO: 내부 코드 로직이 ExcavatorWaveShake.m_SpreadWave() 함수와 유사하므로, 가능하다면, 통합할 수 있는 방법을 모색해보기.
            ExcavatorWave wave = GameObject.Instantiate<ExcavatorWave>(instance.wave);
            
            wave.waveStep = instance.waveLength;
            wave.bInstanceReady = true;
        }
    }
}