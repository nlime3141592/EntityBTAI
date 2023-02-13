using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAbilityGun : PlayerAttack
    {
        // fixed data
        private float m_cooltime = 3.0f;

        // variable
        private float m_leftCooltime;

        public PlayerAbilityGun(Player _player)
        : base(_player)
        {
            base.attackRange = new LTRB()
            {
                left = 1.0f,
                top = 1.0f,
                right = 9.0f,
                bottom = 2.0f
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
            else if(player.aController.bEndOfAction && player.parryingDown)
                return PlayerFsm.c_st_EMERGENCY_PARRYING;
            else
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
        }

        private void m_UpdateCooltime()
        {
            if(m_leftCooltime > 0)
                m_leftCooltime -= Time.deltaTime;
        }
    }
}