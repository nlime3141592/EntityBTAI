using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAttackOnAir : PlayerAttack, IBattleState
    {
        // fixed data
        private float[] m_baseDamages = new float[] { 1.0f, 1.0f };
        private PhaseController m_phaser;
        private Timer m_cooltimer;

        // variable
        private bool m_bAttackDown;

        private float m_lookDirX;
        private float m_speed;

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

            m_phaser = new PhaseController(2, 2.0f);
            m_cooltimer = new Timer(0.1f);

            m_phaser.onFirst += OnFirstPhase;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.battleModule.SetBattleState(this);
            player.bFixLookDirX = true;
            player.vm.FreezePosition(true, false);

            m_bAttackDown = false;

            m_phaser.canUpdate = false;

            player.aPhase = m_phaser.Next();
            base.baseDamage = m_baseDamages[m_phaser.current];
            m_cooltimer.Reset();

/*
            // 상태 시작 시 입력 방향을 감지하고 방향 전환을 함.
            float ix = player.axisInput.x;
            if(ix < 0) player.lookDir.x = -1;
            else if(ix > 0) player.lookDir.x = 1;
            m_lookDirX = player.lookDir.x;
*/
            m_speed = data.jumpOnAirAttackSpeed;
        }

        public virtual void OnFirstPhase()
        {
            --player.leftAirAttackCount;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            // 피격 판정 시에만 방향 전환 고정.
            float ix = player.axisInput.x;
            if(!player.aController.bBeginOfAction || player.aController.bEndOfAction)
                player.lookDir.x = ix < 0 ? -1 : 1;
            m_lookDirX = player.lookDir.x;

            player.vm.SetVelocityXY(0.0f, -1.0f);

            m_speed += data.gravityOnAirAttack * Time.fixedDeltaTime;
            player.vm.SetVelocityY(m_speed);
        }

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();
            m_cooltimer.OnUpdate();
            m_phaser.OnUpdate();

            player.DEBUG_COYOTE = m_phaser.leftCoyoteTime;

            if(!m_phaser.bEndOfCoyoteTime && player.leftAirAttackCount == data.maxAirAttackCount)
                m_phaser.Reset();
            if(m_phaser.canUpdate && m_phaser.bEndOfCoyoteTime)
                m_phaser.Reset();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!m_bAttackDown && player.skill00)
                m_bAttackDown = true;
        }

        public override bool CanTransit()
        {
            if(!m_cooltimer.bEndOfTimer)
                return false;
            else if(m_phaser.bRunning)
                return true;
            else
                return player.leftAirAttackCount > 0;
        }

        public override void OnActionEnd()
        {
            base.OnActionEnd();
            
            m_phaser.canUpdate = true;
            m_phaser.SetCoyote();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(m_bAttackDown)
                return PlayerFsm.c_st_ATTACK_ON_AIR;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.bFixLookDirX = false;
            player.vm.FreezePosition(false, false);

            if(!m_phaser.canUpdate)
            {
                m_phaser.SetCoyote();
                m_phaser.canUpdate = true;
            }
        }
    }
}