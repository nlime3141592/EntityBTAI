using System;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public class PlayerAbilitySword : PlayerAttack //, IBattleState
    {
        // fixed data
        private float m_cooltime = 3.0f;

        // variable
        private float m_leftCooltime;

        public override void OnConstruct()
        {
            base.OnConstruct();
/*
            base.attackRange = new LTRB()
            {
                left = 1.0f,
                top = 1.0f,
                right = 4.0f,
                bottom = 2.0f
            };
            base.targetCount = 7;
            base.baseDamage = 1.0f;
*/
        }

        public override bool CanTransit()
        {
            bool canAttack = m_leftCooltime <= 0;
            return canAttack;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            // instance.battleModule.SetBattleState(this);

            instance.bFixedLookDirByAxis.x = true;
            instance.vm.FreezePositionX();

            m_leftCooltime = m_cooltime;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.vm.SetVelocityXY(0.0f, -1.0f);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAction && instance.parryingDown)
                return Player.c_st_EMERGENCY_PARRYING;

            // NOTE: 디버그용 상태 전환 코드.
            else if(Input.GetKeyDown(KeyCode.Q))
                instance.battleModule.TriggerBattleState();
            else if(Input.GetKeyDown(KeyCode.W))
                instance.aController.bEndOfAnimation = true;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();
            m_UpdateCooltime();
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.bFixedLookDirByAxis.x = false;
            instance.vm.MeltPositionX();
        }

        private void m_UpdateCooltime()
        {
            if(m_leftCooltime > 0)
                m_leftCooltime -= Time.deltaTime;
        }
    }
}