using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAttackOnAir : PlayerAttack, IBattleState
    {
        // fixed data
        private float m_cooltime = 0.1f;

        // variable
        private float m_leftCooltime;

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

        public override bool CanTransit()
        {
            bool canAttack = m_leftCooltime <= 0;
            return canAttack;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.battleModule.SetBattleState(this);

            player.bFixLookDirX = true;
            player.vm.FreezePositionX();
            player.vm.FreezePositionY();

            m_leftCooltime = m_cooltime;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityXY(0.0f, -1.0f);
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