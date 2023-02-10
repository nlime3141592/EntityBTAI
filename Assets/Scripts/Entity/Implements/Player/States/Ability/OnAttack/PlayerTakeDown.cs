using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerTakeDown : PlayerAttack
    {
        // property
        public int maxActionPhase => m_maxActionPhase;

        // fixed data
        private int m_maxActionPhase = 3;
        private float m_cooltime;

        // variable
        private int m_actionPhase;
        private float m_leftCooltime;

        private bool m_bParryingDown;

        public PlayerTakeDown(Player _player)
        : base(_player)
        {
            base.attackRange = new LTRB()
            {
                left = 3.0f,
                top = 0.5f,
                right = 3.0f,
                bottom = 3.0f
            };
            base.targetCount = 7;
            base.baseDamage = 0.7f;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.battleModule.SetBattleState(this);
            player.bFixLookDirX = true;

            if(m_actionPhase >= m_maxActionPhase || m_actionPhase < 0)
                m_actionPhase = 0;

            ++m_actionPhase;
            player.aPhase = m_actionPhase;

            player.vm.FreezePosition(m_actionPhase != 2, m_actionPhase == 1);

            m_leftCooltime = m_cooltime;

            m_bParryingDown = false;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityX(0.0f);

            if(m_actionPhase == 1)
                player.vm.SetVelocityY(0.0f);
            else if(m_actionPhase == 2)
                player.vm.SetVelocityY(-30.0f);
            else if(m_actionPhase == 3)
                player.vm.SetVelocityY(-1.0f);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(player.aController.bEndOfAction)
            {
                if(player.parryingDown)
                    m_bParryingDown = true;
            }
        }

        public override int Transit()
        {
            if(m_actionPhase == 1 && player.aController.bEndOfAnimation)
                return PlayerFsm.c_st_TAKE_DOWN;
            else if(m_actionPhase == 2 && player.senseData.bOnFloor)
                return PlayerFsm.c_st_TAKE_DOWN;
            else if(m_actionPhase == 3)
            {
                int transit = base.Transit();

                if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                    return transit;
                else if(m_bParryingDown)
                    return PlayerFsm.c_st_EMERGENCY_PARRYING;
            }

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            if(m_actionPhase == 3)
                player.bFixLookDirX = false;
        }

        public void UpdateCooltime()
        {
            if(m_leftCooltime > 0)
                m_leftCooltime -= Time.deltaTime;
        }
    }
}