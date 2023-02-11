using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAttackOnAir : PlayerAttack, IBattleState
    {
        // fixed data
        private int m_maxActionPhase = 2;
        private float m_cooltime = 0.1f;
        private float[] m_baseDamages = new float[] { 1.0f, 1.0f };
        private float m_coyoteTime = 2.0f;

        // variable
        private float m_leftCooltime;
        private int m_actionPhase = 0;

        private float m_lookDirX;

        public PlayerAttackOnAir(Player _player)
        : base(_player)
        {
            base.attackRange = new LTRB()
            {
                left = 1.0f,
                top = 1.0f,
                right = 1.0f,
                bottom = 4.0f
            };
            base.targetCount = 7;
            base.baseDamage = 1.0f;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.battleModule.SetBattleState(this);

            player.bFixLookDirX = true;
            player.vm.FreezePositionX();
            player.vm.FreezePositionY();

            if(m_actionPhase >= m_maxActionPhase || m_actionPhase < 0)
                m_actionPhase = 0;

            ++m_actionPhase;
            player.aPhase = m_actionPhase;
            base.baseDamage = m_baseDamages[m_actionPhase - 1];

            float ix = player.axisInput.x;
            if(ix < 0) player.lookDir.x = -1;
            else if(ix > 0) player.lookDir.x = 1;
            m_lookDirX = player.lookDir.x;

            m_leftCooltime = m_cooltime;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityXY(0.0f, -1.0f);
        }

        public override bool CanTransit()
        {
            bool canAttack = m_leftCooltime <= 0;
            return canAttack;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;

            // NOTE: 디버그용 상태 전환 코드.
            else if(Input.GetKeyDown(KeyCode.Q))
                player.battleModule.TriggerBattleState();
            else if(Input.GetKeyDown(KeyCode.W))
                player.aController.bEndOfAnimation = true;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();
            m_UpdateCooltime();
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.bFixLookDirX = false;
            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }

        private void m_UpdateCooltime()
        {
            if(m_leftCooltime > 0)
                m_leftCooltime -= Time.deltaTime;
        }
    }
}